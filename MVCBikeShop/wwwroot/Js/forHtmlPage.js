var tokenKey = "access_token"

function createTableRow(bike) {

    const tr = document.createElement('tr')

    const titleTd = document.createElement('td')
    titleTd.append(bike.bikeTitle)
    titleTd.className = 'showBikes dropdown-toggle';
    // titleTd.setAttribute('data-bs-toggle','dropdown');
    titleTd.value = `${bike.bikeId}`;
    tr.append(titleTd)

    const bikeTd = document.createElement('td')
    bikeTd.className = 'bikeTd'
    bikeTd.value = `${bike.bikeId}`;
    tr.append(bikeTd)

    const sizeTd = document.createElement('td')
    sizeTd.append(bike.size)
    tr.append(sizeTd)

    const speedCountTd = document.createElement('td')
    speedCountTd.append(bike.speedCount)
    tr.append(speedCountTd)

    const wheelDiameterTd = document.createElement('td')
    wheelDiameterTd.append(bike.wheelDiameter)
    tr.append(wheelDiameterTd)

    const priceTd = document.createElement('td')
    priceTd.append(bike.price)
    tr.append(priceTd)

    const photoPathTd = document.createElement('td')
    photoPathTd.append(bike.photoPath)
    tr.append(photoPathTd)

    const manufacturyTd = document.createElement('td')
    manufacturyTd.append(bike.manufacturyId)
    tr.append(manufacturyTd)

    const typeTd = document.createElement('td')
    typeTd.append(bike.typeId)
    tr.append(typeTd)

    const materialTd = document.createElement('td')
    materialTd.append(bike.materialId)
    tr.append(materialTd)

    const breakTypeTd = document.createElement('td')
    breakTypeTd.append(bike.breakTypeId)
    tr.append(breakTypeTd)

    //const manufacturyTd = document.createElement('td')
    //const manufacturyResponse = await fetch(`/api/manufactury/${bike.manufacturyId}`)
    //if (manufacturyResponse.ok === true) {
    //    var manufactury = await manufacturyResponse.json()
    //    console.log(manufactury.manufacturyTitle)
    //    manufacturyTd.append(manufactury.manufacturyTitle)
    //    tr.append(manufacturyTd)
    //}
    //const typeTd = document.createElement('td')
    //const typeResponse = await fetch(`/api/type/${bike.typeId}`)
    //if (typeResponse.ok === true) {
    //    const type = await typeResponse.json()
    //    typeTd.append(type.typeTitle)
    //    tr.append(typeTd)
    //}
    return tr;
}



async function getBikes() {
    const token = sessionStorage.getItem(tokenKey)
    const response = await fetch('/api/bike', {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })
    if (response.ok === true) {
        const bikes = await response.json()
        let rows = document.querySelector('tbody')
        rows.innerHTML = '';
        bikes.forEach(bike => rows.append(createTableRow(bike)))

        let showBikes = document.getElementsByClassName('showBikes')
        showBikes = Array.from(showBikes)

        showBikes.forEach(title => title.addEventListener('mouseover', async function (event) {
            event.target.style.color = "orange";
            //console.log(event.target.value)
        }))
        showBikes.forEach(bike => bike.addEventListener('mouseout', function (event) {
            event.target.style.color = "black";
            //console.log(event.target.value)
        }))
        showBikes.forEach(title => title.addEventListener('click', async function (event) {
            var bike = await getBike(event.target.value)
            console.log(bike.bikeTitle)
            var html = `<div class="row row-cols-1 m-1">
                            <div class="col mb-3 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="0.3s">
                                <div style="display:flex; flex-direction:column; text-align:center; border:1px solid lightgray;border-radius:5px; width:22vw;">
                                    <div>${bike.bikeTitle}</div>
                                    <img src="${bike.photoPath}" />
                                    <div><h6>${bike.price} UAH</h6></div>
                                    <div class="d-flex flex-row justify-content-around p-2">
                                        <button value=${bike.bikeId} class="editButton btn btn-warning" style="min-width:40%;">Edit</button>
                                        <button value=${bike.bikeId} class="removeButton btn btn-danger" style="min-width:40%;">Remove</button>
                                    </div>
                                </div>
                            </div>
                        </div>`

            let bikeTd = document.getElementsByClassName('bikeTd')
            bikeTd = Array.from(bikeTd)
            //bikeTd.forEach(td=>console.log(td.value))
            //console.log(bikeTd)
            for (let i = 0; i < bikeTd.length; i++) {
                if (bikeTd[i].value == event.target.value) {
                    if (bikeTd[i].innerHTML.length > 1) {
                        bikeTd[i].innerHTML = '';
                    }
                    else {
                        bikeTd[i].innerHTML = html;
                    }
                    break;
                }
            }
            //let tr = document.createElement('tr')
            //let td = document.createElement('td')
            //td.innerHTML = html;
            //tr.append(td)
            //rows.append(tr)

            let editButtons = document.getElementsByClassName('editButton')
            editButtons = Array.from(editButtons);
            editButtons.forEach(editButton => {
                editButton.addEventListener('click', async function (e) {

                    var bike = await getBike(e.target.value)

                    document.getElementById('modalForm').click()
                    await bikeToEdit(bike.bikeId, bike.bikeTitle, bike.manufacturyId, bike.typeId, bike.materialId, bike.breakTypeId, bike.speedCount, bike.size, bike.wheelDiameter, bike.price, bike.photoPath)
                    //window.scrollTo({ top: 0, behavior: 'smooth' });
                    var submit = document.getElementById('submit')
                    submit.style.display = 'none';

                    var editBike = document.getElementById('editBike')
                    editBike.style.display = 'inline';
                    await console.log('Edit was clicked')
                })
            })

            let removeButtons = document.getElementsByClassName('removeButton')
            removeButtons = Array.from(removeButtons);
            removeButtons.forEach(removeButton => {
                removeButton.addEventListener('click', async function (e) {
                    //await console.log(e.target.value)
                    await deleteBike(e.target.value)
                    await console.log('Remove was clicked')
                })
            })

        }))

    }
}

async function getBike(id) {

    const token = sessionStorage.getItem(tokenKey)
    const response = await fetch(`/api/bike/${id}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })
    if (response.ok === true) {
        const bike = await response.json()
        //console.log(bike)
        return bike;
    }
}

async function getManufacturies() {
    let myManufacturies = document.getElementById('manufactury')
    const response = await fetch('https://localhost:44348/api/manufactury')
    if (response.ok === true) {
        const manufacturies = await response.json()
        manufacturies.forEach(manufactury => myManufacturies.append(new Option(manufactury.manufacturyTitle, manufactury.manufacturyId)))
    }
}
async function getTypes() {
    let myTypes = document.getElementById('type')
    const response = await fetch('https://localhost:44348/api/type')
    if (response.ok === true) {
        const types = await response.json()
        types.forEach(type => myTypes.append(new Option(type.typeTitle, type.typeId)))
    }
}
async function getMaterials() {
    let myMaterials = document.getElementById('material')
    const response = await fetch('https://localhost:44348/api/material')
    if (response.ok === true) {
        const materials = await response.json()
        materials.forEach(material => myMaterials.append(new Option(material.materialTitle, material.materialId)))
    }
}
async function getBreakTypes() {
    let myBreakTypes = document.getElementById('breakType')
    const response = await fetch('https://localhost:44348/api/break')
    if (response.ok === true) {
        const breakTypes = await response.json()
        breakTypes.forEach(breakType => myBreakTypes.append(new Option(breakType.breakTypeTitle, breakType.breakTypeId)))
    }
}

function setSelectValues(select, selectedValue) {

    const options = Array.from(select.options);
    options.forEach((option, i) => {
        if (option.value == selectedValue)
            select.selectedIndex = i;
    });
}


async function bikeToEdit(bikeId, bikeTitle, manufacturyId, typeId, materialId, breakTypeId, speedCount, size, wheelDiameter, price, photoPath) // data on modalwindow
{
    const form = document.forms['bikeForm']

    let formBikeId = form.elements['bikeId']
    let formBikeTitle = form.elements['bikeTitle']
    let formManufactury = form.elements['manufactury']
    let formType = form.elements['type']
    let formMaterial = form.elements['material']
    let formBreakType = form.elements['breakType']
    let formSpeedCount = form.elements['speedCount']
    let formSize = form.elements['size']
    let formWheelDiameter = form.elements['wheelDiameter']
    let formPhotoPath = form.elements['photoPath']
    let formPrice = form.elements['price']

    formBikeId.value = bikeId;
    formBikeTitle.value = bikeTitle;
    formSpeedCount.value = speedCount;
    formSize.value = size;
    formWheelDiameter.value = wheelDiameter;
    formPhotoPath.value = photoPath;
    formPrice.value = price;


    setSelectValues(formManufactury, manufacturyId)

    setSelectValues(formType, typeId)

    setSelectValues(formMaterial, materialId)

    setSelectValues(formBreakType, breakTypeId)
}

async function createBike(bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price) {
    const token = sessionStorage.getItem(tokenKey)
    const response = await fetch('/api/bike', {
        method: 'POST',
        headers: {
            'Authorization': `bearer ${token}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            bikeTitle,
            manufacturyId: +manufactury,
            typeId: +type,
            materialId: +material,
            breakTypeId: +breakType,
            speedCount: +speedCount,
            size: +size,
            wheelDiameter: +wheelDiameter,
            photoPath,
            price: +price
        })
    })
    //console.log(resonse)
    if (response.ok === true) {
        document.getElementById('errors').style.display = 'none'

        const bike = await response.json()
        document.querySelector('tbody').append(createTableRow(bike))
        //BikesToDisplay();
    } else {
        const errorData = await response.json()

        return errorData
        //document.getElementById('errors').innerHTML = ''

        ////console.log(errorData)
        //if (errorData) {
        //    for (var error in errorData) {
        //        showError(errorData[error])
        //       // console.log(errorData[error])
        //    }
        //    if (errorData['DatasModelError']) {
        //        showError(errorData.DatasModelError)
        //        //console.log(errorData.DatasModelError)
        //    }
        //    if (errorData['TitleError']) {
        //        console.log(errorData.TitleError)
        //    }
        //    document.getElementById('errors').style.display = 'block'
        //}
    }
}

async function showError(error) {
    //const p = document.createElement('p')
    //p.append(error)
    const li = document.createElement('li')
    li.className = 'list-group-item'
    li.append(error)
    li.style.color = 'red'
    document.getElementById('errors').append(li);
}

async function viewError(errorData) {
    if (errorData) {
        console.log(errorData)
        document.getElementById('errors').innerHTML = ''

        if (errorData.errors) { // system errors
            console.log('system errors')
            //console.log(errorData.errors)
            for (var error in errorData.errors) {
                showError(errorData.errors[error])
                console.log(errorData.errors[error])
            }
        }
        else {
            console.log('my ModelState errors')
            for (var error in errorData) {
                showError(errorData[error])
                console.log(errorData[error])
            }
        }


        document.getElementById('errors').style.display = 'block'
        console.log(`bike wasn't added or edited`)
    }
    else {
        getBikes();
        // BikesToDisplay();
        console.log('bike was added')
        document.getElementById('closeButton').click();
    }
}

async function editBikePUT(bikeId, bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price) {

    const token = sessionStorage.getItem(tokenKey)
    const response = await fetch('/api/bike', {
        method: 'PUT',
        headers: {
            'Authorization': `bearer ${token}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            bikeId: +bikeId,
            bikeTitle,
            manufacturyId: +manufactury,
            typeId: +type,
            materialId: +material,
            breakTypeId: +breakType,
            speedCount: +speedCount,
            size: +size,
            wheelDiameter: +wheelDiameter,
            photoPath,
            price: +price
        })
    })
    if (response.ok === true) {
        getBikes();
        // BikesToDisplay();
    }
    else {
        const errorData = await response.json()
        return errorData
    }
}
async function deleteBike(bikeId) {
    const token = sessionStorage.getItem(tokenKey)
    const response = await fetch(`/api/bike/${bikeId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `bearer ${token}`
        }
    })
    if (response.ok === true) {
        getBikes();
        //BikesToDisplay();
    }
}


document.forms['bikeForm'].addEventListener('submit', async function (e) { //create element
    e.preventDefault()
    const form = document.forms['bikeForm']
    const bikeTitle = form.elements['bikeTitle'].value
    const manufactury = form.elements['manufactury'].value
    const type = form.elements['type'].value
    const material = form.elements['material'].value
    const breakType = form.elements['breakType'].value
    const speedCount = form.elements['speedCount'].value
    const size = form.elements['size'].value
    const wheelDiameter = form.elements['wheelDiameter'].value
    const photoPath = form.elements['photoPath'].value
    const price = form.elements['price'].value

    var errorData = await createBike(bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price)

    //console.log(errorData)
    if (errorData) {
        viewError(errorData)
    }
    else {
        getBikes();
        // BikesToDisplay();
        console.log('bike was added')
        document.getElementById('closeButton').click();
    }

})

let editBike = document.getElementById('editBike') // edit button update element
editBike.addEventListener('click', async function (e) {
    e.preventDefault()
    const form = document.forms['bikeForm']
    const bikeId = form.elements['bikeId'].value
    const bikeTitle = form.elements['bikeTitle'].value
    const manufactury = form.elements['manufactury'].value
    const type = form.elements['type'].value
    const material = form.elements['material'].value
    const breakType = form.elements['breakType'].value
    const speedCount = form.elements['speedCount'].value
    const size = form.elements['size'].value
    const wheelDiameter = form.elements['wheelDiameter'].value
    const photoPath = form.elements['photoPath'].value
    const price = form.elements['price'].value

    var errorData = await editBikePUT(bikeId, bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price)

    //console.log(errorData)
    if (errorData) {
        viewError(errorData)
    }
    else {
        getBikes();
        //BikesToDisplay();
        document.getElementById('closeButton').click();
        console.log('bike was edited')
    }
})

getManufacturies()
getTypes()
getMaterials()
getBreakTypes()

//getBikes()



var photoPath = document.getElementById('photoPath') // adding the photo with 'photoPath' source
var forImg = document.getElementById('forImg')
var photo = document.createElement('img')
photoPath.addEventListener('keyup', function () {
    photo.style.width = '80%'
    photo.src = photoPath.value
    forImg.append(photo)
})


var modalForm = document.getElementById('modalForm');                       //add new bike button open MODAL FORM
modalForm.addEventListener('click', function () {
    let createButton = document.getElementById('submit')
    let editButton = document.getElementById('editBike')
    editButton.style.display = 'none';
    createButton.style.display = 'inline';
    document.getElementById('bikeId').value = 0;
    document.getElementById('bikeTitle').value = "";
    document.getElementById('speedCount').value = "";
    document.getElementById('size').value = "";
    document.getElementById('wheelDiameter').value = "";
    document.getElementById('photoPath').value = "";
    document.getElementById('price').value = '';
    document.getElementById('forImg').innerHTML = '';
    document.getElementById('errors').innerHTML = '';

})

var myContainer = document.getElementById("myContainer")                                                   //all bikes with remove and edit buttons
async function BikesToDisplay() {
    var html = `<div class="row row-cols-xl-4 row-cols-md-3 row-cols-sm-2 row-cols-1 m-5">`

    const response = await fetch('/api/bike')
    if (response.ok === true) {
        const bikes = await response.json()
        bikes.forEach(bike => {
            html += ` 
    <div class="col mb-3 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="0.3s">
        <div style="display:flex; flex-direction:column; text-align:center; border:1px solid lightgray;border-radius:5px;">
            <div>${bike.bikeTitle}</div>
            <img src="${bike.photoPath}" />
            <div><h6>${bike.price} UAH</h6></div>
            <div class="d-flex flex-row justify-content-around p-2">
                <button value=${bike.bikeId} class="editButton btn btn-warning" style="min-width:40%;">Edit</button>
                <button value=${bike.bikeId} class="removeButton btn btn-danger" style="min-width:40%;">Remove</button>
            </div>
        </div>
    </div>`
        })
        html += `</div>`
        //let div = document.createElement('div')
        //div.innerHTML = html;

        //console.log(html);
        // myContainer.appendChild(div);
        myContainer.innerHTML = html;


        let editButtons = document.getElementsByClassName('editButton')
        editButtons = Array.from(editButtons);
        editButtons.forEach(editButton => {
            editButton.addEventListener('click', async function (e) {

                var bike = await getBike(e.target.value)

                document.getElementById('modalForm').click()
                await bikeToEdit(bike.bikeId, bike.bikeTitle, bike.manufacturyId, bike.typeId, bike.materialId, bike.breakTypeId, bike.speedCount, bike.size, bike.wheelDiameter, bike.price, bike.photoPath)
                //window.scrollTo({ top: 0, behavior: 'smooth' });
                var submit = document.getElementById('submit')
                submit.style.display = 'none';

                var editBike = document.getElementById('editBike')
                editBike.style.display = 'inline';
                await console.log('Edit was clicked')
            })
        })

        let removeButtons = document.getElementsByClassName('removeButton')
        removeButtons = Array.from(removeButtons);
        removeButtons.forEach(removeButton => {
            removeButton.addEventListener('click', async function (e) {
                //await console.log(e.target.value)
                await deleteBike(e.target.value)
                await console.log('Remove was clicked')
            })
        })

    }
}

//BikesToDisplay();

async function getTokenAsync() {
    const credentials = {
        email: document.getElementById('email').value,
        password: document.getElementById('password').value
    }

    const response = await fetch('/api/accountjwt/token', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    })

    const data = await response.json()
    if (response.ok === true) {
        sessionStorage.setItem(tokenKey, data.access_token)
        getBikes()
    } else {
        console.log(response.status, response.errorText)
    }
}


document.getElementById('submitLogin').addEventListener('click', function () {
    console.log('click')
    getTokenAsync()
})

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

document.forms['sendForm'].addEventListener('submit', async function (e) { //create element
    e.preventDefault()
    const form = document.forms['sendForm']
    const photo_Base64 = form.elements['photo_Base64'].files[0];
    const name = form.elements['name'].value;

   // const id = form.elements['id'].value;

    //var value = btoa(photo_Base64);
    var value = await toBase64(photo_Base64);
    value = value.split(',')[1]
    //console.log(value)

    const formData = {
        name: name,
        photo_Base64: value
    }
   
    const response = await fetch('/api/base64', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
    if (response.ok === true) {
        console.log(response.json())
        console.log('Ok')
    }
    else {
        console.log(response)

        console.log('failed')
    }
})

var getBase64Photos = document.getElementById('getBase64Photos')
getBase64Photos.addEventListener('click', async function () {
    const response = await fetch('/api/base64')
    if (response.ok === true) {
        const photos = await response.json()
       //console.log(photos.filesToDecode)
        //var forPhotos = document.getElementById('forPhotos')
        //var html = '';
        //photos.filesToDecode.forEach(photo => {
        //    html += `<img style='width:20%;' src="data:image/jpeg;base64,${photo}"/>`
        //})
        //forPhotos.innerHTML = html
        var carousel = document.getElementsByClassName('carousel-inner')[0]
        carousel.innerHTML = '';
        if (carousel) {
            console.log('carousel was founded')
        }
        for (let i = 0; i < photos.filesToDecode.length; i++) {
            var div = document.createElement('div')
            if (i == 0) {
                div.className = 'carousel-item active'
                div.innerHTML = `<img class="d-block" style="width:100%;" src="data:image/jpeg;base64,${photos.filesToDecode[i]}">`
            }
            else {
                div.className = 'carousel-item'
                div.innerHTML = `<img class="d-block" style="width:100%;" src="data:image/jpeg;base64,${photos.filesToDecode[i]}">`
            }
            carousel.append(div)

        }
        //var html = '';
        //photos.filesToDecode.forEach(photo => {
        //    var div = document.createElement('div')
        //    div.className = 'carousel-item active' 
        //    div.innerHTML = `<img class="d-block w-100" src="data:image/jpeg;base64,${photo}">`
        //    carousel.append(div)
        //    //html +=`<div class="carousel-item active">
        //    //    <img class="d-block w-100" src="data:image/jpeg;base64,${photo}">
        //    //</div>`
        //})
        //carousel.innerHTML = html
        //console.log(carousel.innerHTML)

        //html += ` ${window.atob(photos.filesToDecode[3])}`
        //forPhotos.innerHTML = html

    }
})

//var convertToBase64 = document.getElementById('convertToBase64')
//convertToBase64.addEventListener('click', async function () {
//    console.log('click')

//    let photo_Base64 = document.getElementById('photo_Base64').files[0];
//    var data = new FormData();
//    data.append(0,photo_Base64)
//    const response = await fetch('/api/base64', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(data)
//    })
//    //const response = await fetch('/api/base64', {
//    //    method: 'GET',
//    //    headers: {
//    //        'Content-Type': 'application/json'
//    //    }
//    //})
//    if (response.ok === true) {
//        console.log(response.json())
//        console.log('Ok')
//    }
//    else {
//        console.log(response)

//        console.log('failed')
//    }
//            //var type = file.type;
//   // console.log(file)
//})






        //   fetch('/api/bike')
        //.then((response) => {
        //    return response.json();
        //})
        //.then((data) => {
        //    console.log(data);
        //});


//if (!editButtons) { alert('меня нет на странице'); } else { alert('я присутствую'); }



//let editButtons = document.getElementsByClassName('editButton')
//editButtons = Array.from(editButtons);
//editButtons.forEach(editButton => {
//    editButton.addEventListener('click', function (e) {
//        console.log(e.target.value)
//        console.log('Edit was clicked')
//    })
//})
//let removeButtons = document.getElementsByClassName('removeButton')
//removeButtons = Array.from(removeButtons);
//removeButtons.forEach(removeButton => {
//    removeButton.addEventListener('click',async function (e) {
//        await console.log(e.target.value)
//    })
//})








//__________________________________________________________________________
//function createTableRow(bike) {

//    const tr = document.createElement('tr')

//    const titleTd = document.createElement('td')
//    titleTd.append(bike.bikeTitle)
//    titleTd.className = 'showBikes dropdown-toggle';
//   // titleTd.setAttribute('data-bs-toggle','dropdown');
//    titleTd.value = `${bike.bikeId}`;
//    tr.append(titleTd)

//    const bikeTd = document.createElement('td')
//    bikeTd.className = 'bikeTd'
//    bikeTd.value = `${bike.bikeId}`;
//    tr.append(bikeTd)

//    const sizeTd = document.createElement('td')
//    sizeTd.append(bike.size)
//    tr.append(sizeTd)

//    const speedCountTd = document.createElement('td')
//    speedCountTd.append(bike.speedCount)
//    tr.append(speedCountTd)

//    const wheelDiameterTd = document.createElement('td')
//    wheelDiameterTd.append(bike.wheelDiameter)
//    tr.append(wheelDiameterTd)

//    const priceTd = document.createElement('td')
//    priceTd.append(bike.price)
//    tr.append(priceTd)

//    const photoPathTd = document.createElement('td')
//    photoPathTd.append(bike.photoPath)
//    tr.append(photoPathTd)

//    const manufacturyTd = document.createElement('td')
//    manufacturyTd.append(bike.manufacturyId)
//    tr.append(manufacturyTd)

//    const typeTd = document.createElement('td')
//    typeTd.append(bike.typeId)
//    tr.append(typeTd)

//    const materialTd = document.createElement('td')
//    materialTd.append(bike.materialId)
//    tr.append(materialTd)

//    const breakTypeTd = document.createElement('td')
//    breakTypeTd.append(bike.breakTypeId)
//    tr.append(breakTypeTd)

//    //const manufacturyTd = document.createElement('td')
//    //const manufacturyResponse = await fetch(`/api/manufactury/${bike.manufacturyId}`)
//    //if (manufacturyResponse.ok === true) {
//    //    var manufactury = await manufacturyResponse.json()
//    //    console.log(manufactury.manufacturyTitle)
//    //    manufacturyTd.append(manufactury.manufacturyTitle)
//    //    tr.append(manufacturyTd)
//    //}
//    //const typeTd = document.createElement('td')
//    //const typeResponse = await fetch(`/api/type/${bike.typeId}`)
//    //if (typeResponse.ok === true) {
//    //    const type = await typeResponse.json()
//    //    typeTd.append(type.typeTitle)
//    //    tr.append(typeTd)
//    //}
//    return tr;
//}



//async function getBikes() {
//    const response = await fetch('https://localhost:44348/api/bike')
//    if (response.ok === true) {
//        const bikes = await response.json()
//        let rows = document.querySelector('tbody')
//        rows.innerHTML = '';
//        bikes.forEach(bike => rows.append(createTableRow(bike)))

//        let showBikes = document.getElementsByClassName('showBikes')
//        showBikes = Array.from(showBikes)

//        showBikes.forEach(title => title.addEventListener('mouseover', async function (event) {
//            event.target.style.color = "orange";
//            //console.log(event.target.value)
//        }))
//        showBikes.forEach(bike => bike.addEventListener('mouseout', function (event) {
//            event.target.style.color = "black";
//            //console.log(event.target.value)
//        }))
//        showBikes.forEach(title => title.addEventListener('click', async function (event) {
//            var bike = await getBike(event.target.value)
//            console.log(bike.bikeTitle)
//            var html = `<div class="row row-cols-1 m-1">
//                            <div class="col mb-3 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="0.3s">
//                                <div style="display:flex; flex-direction:column; text-align:center; border:1px solid lightgray;border-radius:5px; width:22vw;">
//                                    <div>${bike.bikeTitle}</div>
//                                    <img src="${bike.photoPath}" />
//                                    <div><h6>${bike.price} UAH</h6></div>
//                                    <div class="d-flex flex-row justify-content-around p-2">
//                                        <button value=${bike.bikeId} class="editButton btn btn-warning" style="min-width:40%;">Edit</button>
//                                        <button value=${bike.bikeId} class="removeButton btn btn-danger" style="min-width:40%;">Remove</button>
//                                    </div>
//                                </div>
//                            </div>
//                        </div>`

//            let bikeTd = document.getElementsByClassName('bikeTd')
//            bikeTd = Array.from(bikeTd)
//            //bikeTd.forEach(td=>console.log(td.value))
//            //console.log(bikeTd)
//            for (let i = 0; i < bikeTd.length; i++) {
//                if (bikeTd[i].value == event.target.value) {
//                    if (bikeTd[i].innerHTML.length > 1) {
//                        bikeTd[i].innerHTML = '';
//                    }
//                    else {
//                        bikeTd[i].innerHTML = html;
//                    }
//                    break;
//                }
//            }
//            //let tr = document.createElement('tr')
//            //let td = document.createElement('td')
//            //td.innerHTML = html;
//            //tr.append(td)
//            //rows.append(tr)

//            let editButtons = document.getElementsByClassName('editButton')
//            editButtons = Array.from(editButtons);
//            editButtons.forEach(editButton => {
//                editButton.addEventListener('click', async function (e) {

//                    var bike = await getBike(e.target.value)

//                    document.getElementById('modalForm').click()
//                    await bikeToEdit(bike.bikeId, bike.bikeTitle, bike.manufacturyId, bike.typeId, bike.materialId, bike.breakTypeId, bike.speedCount, bike.size, bike.wheelDiameter, bike.price, bike.photoPath)
//                    //window.scrollTo({ top: 0, behavior: 'smooth' });
//                    var submit = document.getElementById('submit')
//                    submit.style.display = 'none';

//                    var editBike = document.getElementById('editBike')
//                    editBike.style.display = 'inline';
//                    await console.log('Edit was clicked')
//                })
//            })

//            let removeButtons = document.getElementsByClassName('removeButton')
//            removeButtons = Array.from(removeButtons);
//            removeButtons.forEach(removeButton => {
//                removeButton.addEventListener('click', async function (e) {
//                    //await console.log(e.target.value)
//                    await deleteBike(e.target.value)
//                    await console.log('Remove was clicked')
//                })
//            })

//        }))

//    }
//}

//async function getBike(id) {
//    const response = await fetch(`/api/bike/${id}`)
//    if (response.ok === true) {
//        const bike = await response.json()
//        //console.log(bike)
//        return bike;
//    }
//}

//async function getManufacturies() {
//    let myManufacturies = document.getElementById('manufactury')
//    const response = await fetch('https://localhost:44348/api/manufactury')
//    if (response.ok === true) {
//        const manufacturies = await response.json()
//        manufacturies.forEach(manufactury => myManufacturies.append(new Option(manufactury.manufacturyTitle, manufactury.manufacturyId)))
//    }
//}
//async function getTypes() {
//    let myTypes = document.getElementById('type')
//    const response = await fetch('https://localhost:44348/api/type')
//    if (response.ok === true) {
//        const types = await response.json()
//        types.forEach(type => myTypes.append(new Option(type.typeTitle, type.typeId)))
//    }
//}
//async function getMaterials() {
//    let myMaterials = document.getElementById('material')
//    const response = await fetch('https://localhost:44348/api/material')
//    if (response.ok === true) {
//        const materials = await response.json()
//        materials.forEach(material => myMaterials.append(new Option(material.materialTitle, material.materialId)))
//    }
//}
//async function getBreakTypes() {
//    let myBreakTypes = document.getElementById('breakType')
//    const response = await fetch('https://localhost:44348/api/break')
//    if (response.ok === true) {
//        const breakTypes = await response.json()
//        breakTypes.forEach(breakType => myBreakTypes.append(new Option(breakType.breakTypeTitle, breakType.breakTypeId)))
//    }
//}

//function setSelectValues(select, selectedValue) {

//    const options = Array.from(select.options);
//    options.forEach((option, i) => {
//        if (option.value == selectedValue)
//            select.selectedIndex = i;
//    });
//}


//async function bikeToEdit(bikeId, bikeTitle, manufacturyId, typeId,materialId, breakTypeId, speedCount, size, wheelDiameter, price, photoPath) // data on modalwindow
//{
//    const form = document.forms['bikeForm']
    
//    let formBikeId = form.elements['bikeId']
//    let formBikeTitle = form.elements['bikeTitle']
//    let formManufactury = form.elements['manufactury']
//    let formType = form.elements['type']
//    let formMaterial = form.elements['material']
//    let formBreakType = form.elements['breakType']
//    let formSpeedCount = form.elements['speedCount']
//    let formSize = form.elements['size']
//    let formWheelDiameter = form.elements['wheelDiameter']
//    let formPhotoPath = form.elements['photoPath']
//    let formPrice = form.elements['price']

//    formBikeId.value = bikeId;
//    formBikeTitle.value = bikeTitle;
//    formSpeedCount.value = speedCount;
//    formSize.value = size;
//    formWheelDiameter.value = wheelDiameter;
//    formPhotoPath.value = photoPath;
//    formPrice.value = price;


//    setSelectValues(formManufactury, manufacturyId)

//    setSelectValues(formType, typeId)

//    setSelectValues(formMaterial, materialId)

//    setSelectValues(formBreakType, breakTypeId)
//}

//async function createBike(bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price) {
//    const response = await fetch('/api/bike', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//            'Accept': 'application/json'
//        },
//        body: JSON.stringify({
//            bikeTitle,
//            manufacturyId: +manufactury,
//            typeId: +type,
//            materialId: +material,
//            breakTypeId: +breakType,
//            speedCount: +speedCount,
//            size: +size,
//            wheelDiameter: +wheelDiameter,
//            photoPath,
//            price: +price
//        })
//    })
//    if (response.ok === true) {
//        document.getElementById('errors').style.display = 'none'

//        const bike = await response.json()
//        document.querySelector('tbody').append(createTableRow(bike))
//        //BikesToDisplay();
//    } else {
//        const errorData = await response.json()

//        return errorData
//        //document.getElementById('errors').innerHTML = ''

//        ////console.log(errorData)
//        //if (errorData) {
//        //    for (var error in errorData) {
//        //        showError(errorData[error])
//        //       // console.log(errorData[error])
//        //    }
//        //    if (errorData['DatasModelError']) {
//        //        showError(errorData.DatasModelError)
//        //        //console.log(errorData.DatasModelError)
//        //    }
//        //    if (errorData['TitleError']) {
//        //        console.log(errorData.TitleError)
//        //    }
//        //    document.getElementById('errors').style.display = 'block'
//        //}
//    }
//}

//async function showError(error) {
//        //const p = document.createElement('p')
//        //p.append(error)
//    const li = document.createElement('li')
//    li.className = 'list-group-item'
//    li.append(error)
//    li.style.color='red'
//    document.getElementById('errors').append(li);
//}

//async function viewError(errorData) {
//    if (errorData) {
//        console.log(errorData)
//        document.getElementById('errors').innerHTML = ''

//        if (errorData.errors) { // system errors
//            console.log('system errors')
//            //console.log(errorData.errors)
//            for (var error in errorData.errors) {
//                showError(errorData.errors[error])
//                console.log(errorData.errors[error])
//            }
//        }
//        else {          
//            console.log('my ModelState errors')
//            for (var error in errorData) {
//                showError(errorData[error])
//                console.log(errorData[error])
//            }
//        }


//        document.getElementById('errors').style.display = 'block'
//        console.log(`bike wasn't added or edited`)
//    }
//    else {
//        getBikes();
//        // BikesToDisplay();
//        console.log('bike was added')
//        document.getElementById('closeButton').click();
//    }
//}

//async function editBikePUT(bikeId,bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price) {
//    const response = await fetch('/api/bike', {
//        method: 'PUT',
//        headers: {
//            'Content-Type': 'application/json',
//            'Accept': 'application/json'
//        },
//        body: JSON.stringify({
//            bikeId:+bikeId,
//            bikeTitle,
//            manufacturyId: +manufactury,
//            typeId: +type,
//            materialId: +material,
//            breakTypeId: +breakType,
//            speedCount: +speedCount,
//            size: +size,
//            wheelDiameter: +wheelDiameter,
//            photoPath,
//            price: +price
//        })
//    })
//    if (response.ok === true) {
//        getBikes();
//        // BikesToDisplay();
//    }
//    else {
//        const errorData = await response.json()
//        return errorData
//    }
//}
//async function deleteBike(bikeId) {
//    const response = await fetch(`/api/bike/${bikeId}`, {
//        method: 'DELETE'
//    })
//    if (response.ok === true) {
//        getBikes();
//        //BikesToDisplay();
//    }
//}


//document.forms['bikeForm'].addEventListener('submit', async function (e) { //create element
//    e.preventDefault()
//    const form = document.forms['bikeForm']
//    const bikeTitle = form.elements['bikeTitle'].value
//    const manufactury = form.elements['manufactury'].value
//    const type = form.elements['type'].value
//    const material = form.elements['material'].value
//    const breakType = form.elements['breakType'].value
//    const speedCount = form.elements['speedCount'].value
//    const size = form.elements['size'].value
//    const wheelDiameter = form.elements['wheelDiameter'].value
//    const photoPath = form.elements['photoPath'].value
//    const price = form.elements['price'].value

//       var errorData= await createBike(bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price)

//    //console.log(errorData)
//    if (errorData) {
//        viewError(errorData)
//    }
//    else {
//        getBikes();
//        // BikesToDisplay();
//        console.log('bike was added')
//        document.getElementById('closeButton').click();
//    }

//})

//let editBike=document.getElementById('editBike') // edit button update element
//editBike.addEventListener('click', async function (e) {
//    e.preventDefault()
//    const form = document.forms['bikeForm']
//    const bikeId = form.elements['bikeId'].value
//    const bikeTitle = form.elements['bikeTitle'].value
//    const manufactury = form.elements['manufactury'].value
//    const type = form.elements['type'].value
//    const material = form.elements['material'].value
//    const breakType = form.elements['breakType'].value
//    const speedCount = form.elements['speedCount'].value
//    const size = form.elements['size'].value
//    const wheelDiameter = form.elements['wheelDiameter'].value
//    const photoPath = form.elements['photoPath'].value
//    const price = form.elements['price'].value

//    var errorData= await editBikePUT(bikeId, bikeTitle, manufactury, type, material, breakType, speedCount, size, wheelDiameter, photoPath, price)

//    //console.log(errorData)
//    if (errorData) {
//        viewError(errorData)
//    }
//    else {
//        getBikes();
//        //BikesToDisplay();
//        document.getElementById('closeButton').click();
//        console.log('bike was edited')
//    }
//})

//getManufacturies()
//getTypes()
//getMaterials()
//getBreakTypes()

//getBikes()



//var photoPath = document.getElementById('photoPath') // adding the photo with 'photoPath' source
//var forImg = document.getElementById('forImg')
//var photo=document.createElement('img')
//photoPath.addEventListener('keyup', function () {
//    photo.style.width='80%'
//    photo.src=photoPath.value
//    forImg.append(photo)
//})


//var modalForm = document.getElementById('modalForm');                       //add new bike button open MODAL FORM
//modalForm.addEventListener('click', function () {
//    let createButton = document.getElementById('submit')
//    let editButton = document.getElementById('editBike')
//    editButton.style.display = 'none';
//    createButton.style.display = 'inline';
//    document.getElementById('bikeId').value = 0;
//    document.getElementById('bikeTitle').value = "";
//    document.getElementById('speedCount').value = "";
//    document.getElementById('size').value = "";
//    document.getElementById('wheelDiameter').value = "";
//    document.getElementById('photoPath').value = "";
//    document.getElementById('price').value = '';
//    document.getElementById('forImg').innerHTML='';
//    document.getElementById('errors').innerHTML = '';

//})

//var myContainer = document.getElementById("myContainer")                                                   //all bikes with remove and edit buttons
//async function BikesToDisplay() {
//    var html = `<div class="row row-cols-xl-4 row-cols-md-3 row-cols-sm-2 row-cols-1 m-5">`

//    const response = await fetch('/api/bike')
//    if (response.ok === true) {
//        const bikes = await response.json()
//        bikes.forEach(bike => {
//            html += ` 
//    <div class="col mb-3 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="0.3s">
//        <div style="display:flex; flex-direction:column; text-align:center; border:1px solid lightgray;border-radius:5px;">
//            <div>${bike.bikeTitle}</div>
//            <img src="${bike.photoPath}" />
//            <div><h6>${bike.price} UAH</h6></div>
//            <div class="d-flex flex-row justify-content-around p-2">
//                <button value=${bike.bikeId} class="editButton btn btn-warning" style="min-width:40%;">Edit</button>
//                <button value=${bike.bikeId} class="removeButton btn btn-danger" style="min-width:40%;">Remove</button>
//            </div>
//        </div>
//    </div>`
//        })
//        html += `</div>`
//        //let div = document.createElement('div')
//        //div.innerHTML = html;

//        //console.log(html);
//       // myContainer.appendChild(div);
//        myContainer.innerHTML = html;


//        let editButtons = document.getElementsByClassName('editButton')
//        editButtons = Array.from(editButtons);
//        editButtons.forEach(editButton => {
//            editButton.addEventListener('click', async function (e) {

//                var bike = await getBike(e.target.value)       

//                document.getElementById('modalForm').click()
//                await bikeToEdit(bike.bikeId, bike.bikeTitle, bike.manufacturyId, bike.typeId,bike.materialId, bike.breakTypeId, bike.speedCount, bike.size, bike.wheelDiameter, bike.price, bike.photoPath)
//                //window.scrollTo({ top: 0, behavior: 'smooth' });
//                var submit = document.getElementById('submit')
//                submit.style.display = 'none';

//                var editBike = document.getElementById('editBike')
//                editBike.style.display = 'inline';
//                await console.log('Edit was clicked')
//            })
//        })

//        let removeButtons = document.getElementsByClassName('removeButton')
//        removeButtons = Array.from(removeButtons);
//        removeButtons.forEach(removeButton => {
//            removeButton.addEventListener('click',async function (e) {
//                //await console.log(e.target.value)
//                await deleteBike(e.target.value)
//                await console.log('Remove was clicked')
//            })
//        })

//    }
//}

////BikesToDisplay();











//        //   fetch('/api/bike')
//        //.then((response) => {
//        //    return response.json();
//        //})
//        //.then((data) => {
//        //    console.log(data);
//        //});


////if (!editButtons) { alert('меня нет на странице'); } else { alert('я присутствую'); }



////let editButtons = document.getElementsByClassName('editButton')
////editButtons = Array.from(editButtons);
////editButtons.forEach(editButton => {
////    editButton.addEventListener('click', function (e) {
////        console.log(e.target.value)
////        console.log('Edit was clicked')
////    })
////})
////let removeButtons = document.getElementsByClassName('removeButton')
////removeButtons = Array.from(removeButtons);
////removeButtons.forEach(removeButton => {
////    removeButton.addEventListener('click',async function (e) {
////        await console.log(e.target.value)
////    })
////})

