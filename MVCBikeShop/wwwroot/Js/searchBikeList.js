//$('#bikeList').load(`/Admin/EditGoodsPartial`)
if ($('#bikeList').html() === "") {
       $('#bikeList').load(`/Admin/EditGoodsPartial`)
}

$('#search').on('click', function () {
    const searchText = $('#searchText').val()
    $('#bikeList').load(`/Admin/EditGoodsPartial/?search=${searchText}`)
});

$('#filterButtonSearch').on('click', function () {
    const manufactury = $('#manufactury').val();
    const type = $('#type').val();
    const wheelDiameters = $('#wheelDiameters').val();
    const material = $('#material').val();
    const speedAmount = $('#speedAmount').val();
    const size = $('#size').val();
    const breakType = $('#breakType').val();
    const priceFrom = $('#priceFrom').val();
    const priceTo = $('#priceTo').val();
    //console.log(priceFrom);
    //console.log(priceTo);
    ///$('#bikeList').load(`/Admin/EditGoodsPartial/?manufactury=${manufactury}`)

    $('#bikeList').load(`/Admin/EditGoodsPartial/?priceFrom=${priceFrom}&priceTo=${priceTo}&manufactury=${manufactury}&type=${type}&wheelDiameters=${wheelDiameters}&material=${material}&speedAmount=${speedAmount}&size=${size}&breakType=${breakType}`)
    //console.log("Click")
});
$('#searchButton').on('click', function () {
    const searchNav = $('#searchNav').val();
    $('#bikeList').load(`/Admin/EditGoodsPartial/?search=${searchNav}`)
})
