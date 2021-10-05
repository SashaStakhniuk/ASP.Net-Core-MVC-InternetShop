async function t() {
    await $('#mainBikeList').load(`/Home/IndexPartial`)
}
var isEmpty = $("#mainBikeList").html() === "";
if (isEmpty) {
   t()
}
//$('#showBikesButton').on('click', function () {
//    console.log("Click")
//    $('#mainBikeList').load(`/Home/IndexPartial`)
//})
function isNumber(n) { return /^-?[\d.]+(?:e-?\d+)?$/.test(n); } 
function indexView(id) {

    const indexManufactury = $('#indexManufactury').val();
    const indextype = $('#indextype').val();
    const indexwheelDiameters = $('#indexwheelDiameters').val();
    const indexmaterial = $('#indexmaterial').val();
    const indexspeedAmount = $('#indexspeedAmount').val();
    const indexsize = $('#indexsize').val();
    const indexbreakType = $('#indexbreakType').val();
    const indexpriceFrom = $('#indexpriceFrom').val();
    const indexpriceTo = $('#indexpriceTo').val();
    if (isNumber(id)) {
        $('#mainBikeList').load(`/Home/IndexPartial/?id=${id}&priceFrom=${indexpriceFrom}&priceTo=${indexpriceTo}&manufactury=${indexManufactury}&type=${indextype}&wheelDiameters=${indexwheelDiameters}&material=${indexmaterial}&speedAmount=${indexspeedAmount}&size=${indexsize}&breakType=${indexbreakType}`)
    }
    else {
        $('#mainBikeList').load(`/Home/IndexPartial/?priceFrom=${indexpriceFrom}&priceTo=${indexpriceTo}&manufactury=${indexManufactury}&type=${indextype}&wheelDiameters=${indexwheelDiameters}&material=${indexmaterial}&speedAmount=${indexspeedAmount}&size=${indexsize}&breakType=${indexbreakType}`)
    }
    //console.log(isNumber(id))
}
$('#indexFilterButtonSearch').on('click', async function ()
    {
        await indexView();
});

let buttonPagination = document.getElementsByClassName('buttonPagination')
buttonPagination = Array.from(buttonPagination);
buttonPagination.forEach(elem => {
    elem.addEventListener('click', async function (e) {
        await indexView(e.target.value);
       // console.log(e.target.value)
    });
});
//buttonPagination.forEach(elem => {
//    elem.addEventListener('click', function (e) {
//        indexView(e.target.value);
//        //console.log(e.target.value)
//    });
//});
//buttonPagination.forEach(elem => {
//    elem.addEventListener('click', function (e) {
//        const indexManufactury = $('#indexManufactury').val();
//        const indextype = $('#indextype').val();
//        const indexwheelDiameters = $('#indexwheelDiameters').val();
//        const indexmaterial = $('#indexmaterial').val();
//        const indexspeedAmount = $('#indexspeedAmount').val();
//        const indexsize = $('#indexsize').val();
//        const indexbreakType = $('#indexbreakType').val();
//        const indexpriceFrom = $('#indexpriceFrom').val();
//        const indexpriceTo = $('#indexpriceTo').val();
//        $('#mainBikeList').load(`/Home/IndexPartial/?id=${e.target.value}&priceFrom=${indexpriceFrom}&priceTo=${indexpriceTo}&manufactury=${indexManufactury}&type=${indextype}&wheelDiameters=${indexwheelDiameters}&material=${indexmaterial}&speedAmount=${indexspeedAmount}&size=${indexsize}&breakType=${indexbreakType}`)
        
//        console.log(e.target.value)
//    });
//});
//$(".buttonPagination").on('click', async function () {
//    var id = $(".buttonPagination").val();
//    console.log(id)
//    await indexView(id);
//});


let infoImg = document.getElementsByClassName('infoImg')
infoImg = Array.from(infoImg)

let infoBlock = document.getElementsByClassName('infoBlock')
infoBlock = Array.from(infoBlock)

infoImg.forEach(info => {
    info.addEventListener('mouseover', async function () {
        info.src = "/images/icons/info.gif"
        var infoBlock = document.getElementsByClassName('infoBlock');
        infoBlock = Array.from(infoBlock)
        infoBlock.forEach(block => {
            if (info.getAttribute('value') === block.getAttribute('value')) {
                block.style.display = 'block'
            }
            //console.log(block.getAttribute('value'))
        })
    })
})

infoImg.forEach(info => {
    info.addEventListener('mouseout', async function () {
        info.src = '/images/icons/info.png'

        infoBlock.forEach(block => {
            block.style.display = 'none';
        })
    })
})

//infoImg.forEach(info => {
//    info.addEventListener('click', async function () {
//        var infoBlock = document.getElementsByClassName('infoBlock');
//        infoBlock = Array.from(infoBlock)
//        infoBlock.forEach(block => {
//            if (info.getAttribute('value') === block.getAttribute('value')) {
//                block.style.display = 'block'
//            }
//            //console.log(block.getAttribute('value'))
//        })
//    })
//})

