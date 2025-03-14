﻿using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace MOEWiki.AutoRecognitionAndParse
{
    internal class Program
    {
        private static Char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
        private static string PrepareForDouble(string source)
        {
            return source.Replace(',', separator).Replace('.', separator);
        }
        private static string GetLastAndAnothrWords(string[] sourse)
        {
            var last = sourse[sourse.Length - 1];
            return (string.Join(' ', sourse.Where(w => CheckIsWord(w) && w != last)) + ' ' + last).Trim();
        }
        private static bool CheckIsWord(string word)
        {
            var regex = new Regex("^[a-zA-z]+[-]?[']?[a-zA-z]+$");
            return regex.IsMatch(word);
        }
        private static ItemProperty ParseWithColon(string text)
        {
            var result = new ItemProperty();
            result.Name = text[..text.IndexOf(':')].Trim();
            result.Value = RemoveSlash(text[(text.IndexOf(":") + 1)..].Trim());
            return result;
        }
        private static string RemoveSlash(string s)
        {
            if (s.IndexOf("/") == -1)
                return s;
            else
                return s[(s.IndexOf("/") + 1)..];
        }
        private static Dictionary<ParseResultEnum, int> ConvertStringToParseResultList(string s)
        {
            return s.Split(';').Where(w => !string.IsNullOrWhiteSpace(w.Replace("\t", "").Trim()) && s.Length > 2).SelectMany(s => s.Split(':').Select(ss => new KeyValuePair<ParseResultEnum, int>((ParseResultEnum)s[0], (int)s[1]))).ToDictionary(p => p.Key, p => p.Value);
        }
        private static string ConvertParseResultListToString(Dictionary<ParseResultEnum, int> list)
        {
            return string.Join(';', list.Select(s => $"{s.Key}:{s.Value}"));
        }
        private static List<ItemProperty> ParseItemProperties(string sourse)
        {
            var properties = new List<ItemProperty>();
            properties.Clear();
            var s = sourse.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
            if (s.Length > 0)
            {
                var quality = QualityEnum.Low;
                var allProperties = GatewaysFacade.PropertyGateway.GetAll().ToList();
                var descrNum = 0;
                for (int i = 1; i < s.Length; i++)
                {
                    if (s[i].StartsWith("Description"))
                    {
                        descrNum = i;
                        break;
                    }
                    else if (s[i - 1].StartsWith("Level:"))
                    {
                    }
                    else if (s[i].StartsWith("Quality"))
                    {
                        switch (s[i].Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).Last().ToLower().Replace(".", "").Replace(" ", ""))
                        {
                            case "lv10":
                                quality = QualityEnum.Lvl10;
                                break;
                            case "lv30":
                                quality = QualityEnum.Lvl30;
                                break;
                            case "lv50":
                                quality = QualityEnum.Lvl50;
                                break;
                            case "lv70":
                                quality = QualityEnum.Lvl70;
                                break;
                            case "lv100":
                                quality = QualityEnum.Lvl100;
                                break;
                            default:
                                quality = QualityEnum.Low;
                                break;
                        }
                        if (quality != QualityEnum.Low)
                        {
                            foreach (var p in properties)
                            {
                                if (p.Name == "Durability")
                                {
                                    p.Quality = quality;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        var prop = allProperties.FirstOrDefault(f => s[i].StartsWith(f.Name));
                        if (prop != null)
                        {
                            var sameStart = allProperties.Where(w => w.Name.StartsWith(prop.Name));
                            if (sameStart.Count() > 1)
                            {
                                foreach (var p in sameStart.OrderByDescending(o => o.Name.Length))
                                {
                                    if (s[i].StartsWith(p.Name))
                                    {
                                        prop = p;
                                        break;
                                    }
                                }
                            }
                            var newIP = new ItemProperty(prop);
                            if (prop.IsDependsByQuality)
                            {
                                newIP.Quality = quality;
                            }
                            switch (prop.PropertyType)
                            {
                                case PropertyTypeEnum.Text:
                                    newIP.Value = RemoveSlash(GetLastAndAnothrWords(s[i].Replace(prop.Name, "").Replace("(", "").Replace(")", "").Replace(':', ' ').Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray()));
                                    break;
                                case PropertyTypeEnum.Percent:
                                case PropertyTypeEnum.Number:
                                    newIP.Value = RemoveSlash(s[i].Replace(prop.Name, "").Replace("(", "").Replace(")", "").Replace(':', ' ').Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).LastOrDefault() ?? "");
                                    break;
                            }
                            properties.Add(newIP);
                        }
                        else
                        {
                            if (s[i].Contains(':'))
                            {
                                var p = ParseWithColon(s[i]);
                                if (!string.IsNullOrWhiteSpace(p.Value))
                                {
                                    properties.Add(p);
                                }
                            }
                            else
                            {
                                var newIP = new ItemProperty();
                                var ss = s[i].Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
                                if (ss.Length == 1)
                                {
                                    newIP.Name = ss[0];
                                    newIP.Value = RemoveSlash(ss[0]);
                                }
                                else
                                {
                                    var last = ss.Last();
                                    newIP.Value = RemoveSlash(last);
                                    newIP.Name = string.Join(' ', ss.Where(w => CheckIsWord(w) && w != last));
                                }
                                properties.Add(newIP);
                            }
                        }
                    }
                }
                if (descrNum != 0)
                {
                    var descrProp = allProperties.FirstOrDefault(f => f.Name == "Description");
                    var value = s[descrNum].Replace("Description:", "").Trim();
                    for (int i = descrNum + 1; i < s.Length; i++)
                    {
                        if (s[i] == s[0]) break;
                        value += ' ' + s[i].Trim();
                    }
                    value += '.';
                    value = value.Replace("..", ".").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");
                    if (descrProp == null)
                    {
                        properties.Add(new ItemProperty() { Name = "Description", Value = value });
                    }
                    else
                    {
                        properties.Add(new ItemProperty(descrProp) { Value = value });
                    }
                }
            }
            return properties;
        }
        private static List<ItemRecipe> ParseItemRecipes(string sourse)
        {
            var recipes = new List<ItemRecipe>();
            var v = sourse.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
            recipes.Clear();
            if (v.Length > 0)
            {
                var descrNum = 0;
                var costNum = 0;
                for (int i = 1; i < v.Length; i++)
                {
                    if (v[i].StartsWith("Cost"))
                    {
                        costNum = i;
                        break;
                    }
                }
                for (int i = costNum + 1; i < v.Length; i++)
                {
                    if (v[i].Contains("Description"))
                    {
                        descrNum = i;
                        break;
                    }
                }
                var isStep = false;
                for (int i = costNum + 1; i <= descrNum - 1; i++)
                {
                    if (v[i].ToLower().StartsWith("step"))
                    {
                        isStep = true;
                        continue;
                    }
                    var newIR = new ItemRecipe() { IsStepByStep = isStep };
                    var s = v[i].Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
                    var last = s.Last();
                    newIR.RecipeItemName = string.Join(' ', s.Where(w => CheckIsWord(w) && w != last));
                    var count = 0;
                    int.TryParse(RemoveSlash(last), out count);
                    newIR.Count = count;
                    var p = GatewaysFacade.ItemGateway.GetByNameSmart(newIR.RecipeItemName);
                    if (p != null)
                    {
                        newIR.RecipeItemName = p.Name;
                        newIR.RecipeItemId = p.Id;
                    }
                    recipes.Add(newIR);
                }
            }
            return recipes;
        }
        private static string CompareItemProperties(Item item, List<ItemProperty> parsedP)
        {
            var redCount = 0; //new
            var blackCount = 0; //update
            var orangeCount = 0; //delete
            string result = string.Empty;
            var itemProperties = GatewaysFacade.ItemPropertyGateway.GetAllByItemId(item.Id).OrderBy(o => o.SortId);
            foreach (var property in parsedP.Where(w => w.Quality != QualityEnum.Low))
            {
                var inBoth = itemProperties.Where(w => w.PropertyId == property.PropertyId);
                foreach (var ib in inBoth)
                {
                    if (!(ib.Quality != property.Quality || ib.Value == property.Value))
                    {
                        orangeCount++;
                    }
                }
                if (!itemProperties.Any(w => w.PropertyId == property.PropertyId && w.Quality == property.Quality))
                {
                    redCount++;
                }
            }
            foreach (var property in parsedP.Where(w => w.Quality == QualityEnum.Low))
            {
                var inBoth = itemProperties.Where(w => w.PropertyId == property.PropertyId);
                foreach (var ib in inBoth)
                {
                    if (ib.Value != property.Value)
                    {
                        orangeCount++;
                    }
                }
                if (!inBoth.Any())
                {
                    redCount++;
                }
            }
            var ipids = parsedP.Select(s => s.PropertyId);
            blackCount += itemProperties.Count(w => !ipids.Contains(w.PropertyId));
            if (blackCount + redCount + orangeCount == 0)
            {
                return $"{ParseResultEnum.NothingChanged}:{parsedP.Count()};";
            }
            else
            {
                if (blackCount > 0) result += $"{ParseResultEnum.HaveItemPropertiesToDelete}:{blackCount};";
                if (redCount > 0) result += $"{ParseResultEnum.HaveNewItemProperties}:{redCount};";
                if (orangeCount > 0) result += $"{ParseResultEnum.HaveItemPropertiesToUpdate}:{orangeCount};";
            }
            return result;
        }
        private static string CompareItemRecipes(Item item, List<ItemRecipe> parsedR)
        {
            var redCount = 0; //new
            var blackCount = 0; //update
            var orangeCount = 0; //delete
            string result = string.Empty;
            var itemRecipes = GatewaysFacade.ItemRecipeGateway.GetAllByItemId(item.Id);
            foreach (var recipe in itemRecipes)
            {
                var r = parsedR.FirstOrDefault(a => a.RecipeItemId == recipe.RecipeItemId);
                if (r == null)
                {
                    blackCount++;
                }
                else
                {
                    if (!(r.Count == recipe.Count && r.IsStepByStep == recipe.IsStepByStep))
                    {
                        orangeCount++;
                    }
                }
            }
            foreach (var i in parsedR.Where(w => !itemRecipes.Any(a => a.RecipeItemId == w.RecipeItemId)))
            {
                redCount++;
            }
            if (blackCount + redCount + orangeCount == 0)
            {
                return $"{ParseResultEnum.NothingChanged}:{parsedR.Count()};";
            }
            else
            {
                if (blackCount > 0) result += $"{ParseResultEnum.HaveItemRecipiesToDelete}:{blackCount};";
                if (redCount > 0) result += $"{ParseResultEnum.HaveNewItemRecipies}:{redCount};";
                if (orangeCount > 0) result += $"{ParseResultEnum.HaveItemRecipiesToUpdate}:{orangeCount};";
            }
            return result;
        }
        private static int PixelRGBSum(Color color) => color.R + color.G + color.B;
        private static string CompareIcons(Bitmap currentIcon, Bitmap newIcon)
        {
            var v = 0;
            for (var i = 0; i < currentIcon.Width; i++)
            {
                for (var j = 0; j < currentIcon.Height; j++)
                {
                    var p1 =
                    v += PixelRGBSum(currentIcon.GetPixel(i, j)) - PixelRGBSum(newIcon.GetPixel(i, j));
                }
            }
            if (v > 100)
            {
                newIcon.Save($"E:\\МОЕ\\newicon\\{Guid.NewGuid()}.jpg", ImageFormat.Jpeg);
                return $"{ParseResultEnum.ImageChanged}:{v};";
            }
            else
            {
                return "";
            }
        }

        static async Task AsyncMain(string[] args)
        {
            await Task.Factory.StartNew(async()  =>
            {
                while (true)
                {
                    foreach (var scan in GatewaysFacade.AutoParseGateway.GetAll().Where(w => w.ParseState == ParseStateEnum.Scanned).ToList())
                    {
                        if (string.IsNullOrWhiteSpace(scan.ImageName) || !File.Exists(scan.ImageName)) continue;
                        try
                        {
                            HttpClient httpClient = new HttpClient();
                            httpClient.Timeout = new TimeSpan(1, 1, 1);


                            MultipartFormDataContent form = new MultipartFormDataContent();
                            form.Add(new StringContent("K87429841688957"), "apikey"); //Added api key in form data
                                                                                        //form.Add(new StringContent("K81570705388957"), "apikey"); //Added api key in form data

                            form.Add(new StringContent("5"), "ocrengine");
                            form.Add(new StringContent("true"), "scale");
                            form.Add(new StringContent("true"), "istable");

                            byte[] imageData = File.ReadAllBytes(scan.ImageName);
                            form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

                            HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);

                            string strContent = await response.Content.ReadAsStringAsync();



                            Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);


                            if (ocrResult.OCRExitCode == 1)
                            {
                                for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                                {
                                    scan.RecognitionResult += ocrResult.ParsedResults[i].ParsedText;
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR: " + strContent);
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Ooops 2");
                            break;
                        }
                    }
                    var date = DateTime.Now;
                    foreach (var scan in GatewaysFacade.AutoParseGateway.GetAll().Where(w => w.ParseState == ParseStateEnum.Recognized).ToList())
                    {
                        try
                        {
                            scan.ParseState = ParseStateEnum.Parsed;
                            var itemName = scan.RecognitionResult.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).FirstOrDefault();
                            string result = $"{ParseResultEnum.ItemNotFound}:0";
                            var item = new Item();
                            if (itemName == null || string.IsNullOrWhiteSpace(itemName))
                            {
                            }
                            else
                            {
                                item = GatewaysFacade.ItemGateway.GetByNameSmart(itemName);
                                if (item == null || item.Id == 0)
                                {
                                    scan.ParseResult = result;
                                    throw new Exception();
                                }
                                else
                                {
                                    result = "";
                                }
                            }
                            if (scan.Mode == ModeEnum.Item)
                            {
                                var prop = ParseItemProperties(scan.RecognitionResult);
                                if (prop != null)
                                {
                                    if (prop.Any(a => a.PropertyId == 0))
                                    {
                                        scan.ParseResult = $"{ParseResultEnum.HaveNewItemProperties}:{prop.Count(a => a.PropertyId == 0)}";
                                        throw new Exception();
                                    }
                                    scan.ParseResult = CompareItemProperties(item, prop);
                                    var path = $"E:\\МОЕ\\icon\\" + item.ImageName;
                                    if (File.Exists(path))
                                    {
                                        var currentIcon = new Bitmap(Image.FromFile(path));
                                        var s = CompareIcons(currentIcon, new Bitmap(Image.FromFile(scan.ImageName)).Clone(new Rectangle(16, 16, 118, 118), currentIcon.PixelFormat));
                                        if (!string.IsNullOrWhiteSpace(s))
                                        {
                                            scan.ParseResult += s;
                                            Console.WriteLine($"{s}[{item.Id}] {DateTime.Now}");
                                        }
                                    }
                                }
                            }
                            else
                            if (scan.Mode == ModeEnum.Recipe)
                            {
                                var rec = ParseItemRecipes(scan.RecognitionResult);
                                if (rec != null)
                                {
                                    if (rec.Any(a => a.RecipeItemId == 0))
                                    {
                                        scan.ParseResult = $"{ParseResultEnum.HaveNewRecipeItems}:{rec.Count(a => a.RecipeItemId == 0)}";
                                        throw new Exception();
                                    }
                                    scan.ParseResult = CompareItemRecipes(item, rec);
                                }
                            }
                        }
                        catch { }
                        finally
                        {
                            GatewaysFacade.AutoParseGateway.Update(scan);
                        }
                        Console.WriteLine($"Sleep 1 hour");
                        Thread.Sleep(date.AddHours(1) - DateTime.Now);
                    }
                }
            });
        }

        static void Main(string[] args)
        {
            // синхронное ожидание!
            AsyncMain(args).Wait();
        }
    }
}
