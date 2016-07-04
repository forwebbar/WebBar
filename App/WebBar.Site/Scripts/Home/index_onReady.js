$(function() {
    $("#datetimepickerStart").datetimepicker({ locale: 'ru', format: 'DD-MM-YYYY', date: '@Model.FilterDateStart.GetValueOrDefault().ToString("o")' });
    $("#datetimepickerEnd").datetimepicker({ locale: 'ru', format: 'DD-MM-YYYY', date: '@Model.FilterDateEnd.GetValueOrDefault().ToString("o")' });

    $("#datetimepickerStart").on("dp.change", function(e) {
        // $("#datetimepickerEnd").data("DateTimePicker").minDate(e.date);

        var endDate = $("#datetimepickerEnd").data("DateTimePicker").date();
        window.location.href = "/?beerType=@Model.SelectedBeerType&shopId=@Model.CurrentShopId&isDetail=@Model.IsDetail&filterType=Custom&filterDateStart=" + e.date.toISOString() + "&filterDateEnd=" + endDate.toISOString();

    });

    $("#datetimepickerEnd").on("dp.change", function(e) {
        var startDate = $("#datetimepickerStart").data("DateTimePicker").date();
        window.location.href = "/?beerType=@Model.SelectedBeerType&isDetail=@Model.IsDetail&shopId=@Model.CurrentShopId&filterDateStart=" + startDate.toISOString() + "&filterDateEnd=" + e.date.toISOString();


    });


    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

    $(".cleaningChb").click(function() {

        var beerTypeName = "idBeerType";
        var beerTypeValue = getParameterByName(beerTypeName);
        var isBeerTypeCleaningName = "isBeerTypeCleaning";
        var isBeerTypeCleaningValue = getParameterByName(isBeerTypeCleaningName);
        var idSummaryName = "idSummary";
        var idSummaryValue = getParameterByName(idSummaryName);

        var url = window.location.href;

        var newbeerTypeString = beerTypeName + "=" + $(this).attr('data-idBeerType');
        if (beerTypeValue == null)
            url += "&" + newbeerTypeString;
        else
            url = url.replace(beerTypeName + "=" + beerTypeValue, newbeerTypeString);


        var newBeerTypeCleaningString = isBeerTypeCleaningName + "=" + $(this).attr('data-isBeerTypeCleaning');
        if (isBeerTypeCleaningValue == null)
            url += "&" + newBeerTypeCleaningString;
        else
            url = url.replace(isBeerTypeCleaningName + "=" + beerTypeValue, newBeerTypeCleaningString);

        var newSummaryString = idSummaryName + "=" + $(this).attr('data-idSummary');
        if (beerTypeValue == null)
            url += "&" + newSummaryString;
        else
            url = url.replace(idSummaryName + "=" + idSummaryValue, newSummaryString);

        window.location.href = url;

    });
});