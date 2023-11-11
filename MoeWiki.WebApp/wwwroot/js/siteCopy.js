// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Search() {
    window.location.href = '/Home/SearchResult/?searchString=' + $('#searchstring').val();
}

function Find() {
    window.location.href = '/Calculator/FindItem/?searchString=' + $('#searchstring2').val();
}

function TX(x, maxX, minX, w) {
    return (maxX - minX) / w * x + minX;
}
function TY(y, maxY, minY, h) {
    return -((maxY - minY) / h * y + minY);
}

function ResizeMarkers(wh, mlt) {
    $(".leaflet-marker-icon").css('width', wh + 'px').css('height', wh + 'px').css('margin-left', mlt + 'px').css('margin-top', mlt + 'px');
    $(".coailition-icon").css('width', (wh * 1.2) + 'px').css('height', (wh * 1.2) + 'px').css('margin-left', (mlt * 1.2) + 'px').css('margin-top', (mlt * 1.2) + 'px');
}

function ShowHideMarkers(name, display) {
    $(".leaflet-marker-icon[alt='" + name + "']").css('display', display);
    $(".leaflet-image-layer[alt='" + name + "']").css('display', display);
}

function createLineElement(x, y, length, angle) {
    var line = document.createElement("div");
    var styles = 'border: 1px solid black; '
        + 'width: ' + length + 'px; '
        + 'height: 0px; '
        + '-moz-transform: rotate(' + angle + 'deg); '
        + '-webkit-transform: rotate(' + angle + 'deg); '
        + '-o-transform: rotate(' + angle + 'deg); '
        + '-ms-transform: rotate(' + angle + 'deg); '
        + 'position: absolute; '
        + 'z-index: 99; '
        + 'top: ' + y + 'px; '
        + 'left: ' + x + 'px; ';
    line.setAttribute('style', styles);
    return line;
}

function DrawConnect(id, parents, w, h, iw, ih) {
    var child = $('#' + id);
    $(parents).each(function (index, element) {
        var parent = $('#entity_' + element);
        if ($(child).attr('row') == $(parent).attr('row')) {
            $(createLineElement($(parent).css('left').replace('px', '') / 1 + w - iw, $(parent).css('top').replace('px', '') / 1 + (h - ih) / 2, iw + (Math.abs($(child).attr('col') - $(parent).attr('col')) - 1) * w, 0)).appendTo($(child).parent());
        } else {
            $(createLineElement($(parent).css('left').replace('px', '') / 1 + w - iw, $(parent).css('top').replace('px', '') / 1 + (h - ih) / 2, iw / 2, 0)).appendTo($(child).parent());
            $(createLineElement($(parent).css('left').replace('px', '') / 1 + w - iw / 2, $(child).css('top').replace('px', '') / 1 + (h - ih) / 2, iw / 2 + (Math.abs($(child).attr('col') - $(parent).attr('col')) - 1) * w, 0)).appendTo($(child).parent());
            var l = Math.abs($(child).attr('row') - $(parent).attr('row')) * h;
            if ($(child).attr('row') > $(parent).attr('row')) {
                $(createLineElement($(parent).css('left').replace('px', '') / 1 + w - iw / 2 - l / 2, $(child).css('top').replace('px', '') / 1 + (h - ih) / 2 - l / 2, l, 90)).appendTo($(child).parent());
            } else {
                $(createLineElement($(parent).css('left').replace('px', '') / 1 + w - iw / 2 - l / 2, $(child).css('top').replace('px', '') / 1 + (h - ih) / 2 + l / 2, l, 90)).appendTo($(child).parent());
            }
        }
    });
}
function UpdateCheckboxes(element, idPref, className) {
    if ($(element).is(':checked')) {
        CheckPrev(element, idPref);
    } else {
        UncheckNext($(element).attr('entityId'), className);
    }
}

function CheckPrev(element, idPref) {
    $($(element).data('parents')).each(function (index, element) {
        var parent = $("#" + idPref + element);
        $(parent).prop('checked', true);
        CheckPrev(parent, idPref);
    });
}
function UncheckNext(id, className) {
    $("." + className).each(function () {
        var e = $(this);
        $($(e).data('parents')).each(function (index, element) {
            if (element != id) return;
            $(e).prop('checked', false);
            UncheckNext($(e).attr('entityId'), className);
        });
    });
}

function CheckValuesGS(element) {
    UpdateSpan(element);
    EquateParent(element);
    EquateToParent(element);
}

function EquateParent(e) {
    var v = GetParentValueNeed($(e).attr("reqtype"), $(e).val(), $(e).attr("entityId"));
    $($(e).data('parents')).each(function (index, element) {
        var parent = $("#reqgs_" + element);
        if ($(parent).val() < v) {
            $(parent).val(v);
            UpdateSpan(parent);
            EquateParent(parent);
        }
    });
}

function EquateToParent(element) {
    var parentVal = $(element).val();
    var id = $(element).attr("entityId");
    $(".req-skill").each(function () {
        var e = $(this);
        $($(this).data('parents')).each(function (index, element) {
            if (element != id) return;
            var currVal = $(e).val();
            switch ($(e).attr("reqtype")) {
                case 'ByOne':
                    if (parentVal == 0) {
                        currVal = 0;
                    }
                    break;
                case 'Equal1':
                    if (currVal > parentVal) {
                        currVal = parentVal;
                    }
                    break;
                case 'Equal2':
                    if (currVal > (parentVal - parentVal % 2) / 2) {
                        currVal = (parentVal - parentVal % 2) / 2;
                    }
                    break;
                case 'Equal10':
                    if (currVal > (parentVal - parentVal % 10) / 10) {
                        currVal = (parentVal - parentVal % 10) / 10;
                    }
                    break;
                case 'FivePlusOne':
                    if (currVal > (parentVal - parentVal % 5) / 5 - 1) {
                        currVal = (parentVal - parentVal % 5) / 5 - 1;
                    }
                    break;
                case 'TwiseTen':
                    if (currVal > (parentVal - parentVal % 5) / 5 - ((parentVal - parentVal % 5) / 5) % 2) {
                        currVal = (parentVal - parentVal % 5) / 5 - ((parentVal - parentVal % 5) / 5) % 2;
                    }
                    break;
                case 'Unique':
                    $.post("/GuildSkill/EquateToParent", { id: $(e).attr("entityId"), parentVal: parentVal }, function (data) {
                        currVal = data.result;
                    });
                    break;
            };
            if (currVal < $(e).val()) {
                $(e).val(currVal);
                UpdateSpan(e);
                EquateToParent($(e));
            }
        });
    });
}

function UpdateSpan(e) {
    $("#span_" + $(e).attr("id")).text("(" + $(e).val() + ")");
}

function GetParentValueNeed(type, v, id) {
    switch (type) {
        case 'ByZiro':
            return 0;
            break;
        case 'ByOne':
            return 1;
            break;
        case 'Equal1':
            return v;
            break;
        case 'Equal2':
            return v * 2;
            break;
        case 'Equal10':
            return v * 10;
            break;
        case 'FivePlusOne':
            return v * 5 + 5;
            break;
        case 'TwiseTen':
            return (v + v % 2) * 5;
            break;
        case 'Unique':
            return $.post("/GuildSkill/GetParentValueNeed", {id: id, val: v}, function (data) {
                return data.result;
            });  
            break;
    }
}

