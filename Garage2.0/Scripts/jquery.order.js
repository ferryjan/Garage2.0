/**
 * Sort a list of elements and apply the order to the DOM.
 *
 * https://gist.github.com/mindplay-dk/6825439
 */
jQuery.fn.order = function (asc, fn) {
    fn = fn || function (el) {
        return $(el).text().replace(/^\s+|\s+$/g, '');
    };
    var T = asc !== false ? 1 : -1,
        F = asc !== false ? -1 : 1;
    this.sort(function (a, b) {
        a = fn(a), b = fn(b);
        if (a == b) return 0;
        return a < b ? F : T;
    });
    this.each(function (i) {
        this.parentNode.appendChild(this);
    });
};

$(function ($) {
    $("table.sortable th").click(function () {
        $(this).siblings("th").removeClass("ordered asc");
        $(this).addClass("ordered");
        var asc = !$(this).hasClass("asc");
        var idx = $(this).prevAll("th").length;
        $(this).closest("table").find("tr").has("td").order(asc, function (el) {
            return $("td", el).eq(idx).text();
        });
        if (asc) {
            $(this).addClass("asc");
            $(this).removeClass("desc");
        } else {
            $(this).addClass("desc");
            $(this).removeClass("asc");
        }
    });
});

// alphabetical list sort, ascending: (default)

//$('ul li').order();

// sort table rows by descending value in first column:

//$('table tr').order(false, function (el) {
//    return parseInt($('td', el).text());
//});
