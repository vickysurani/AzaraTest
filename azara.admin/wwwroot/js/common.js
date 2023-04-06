function openProfileDropDown() {
    var className = $('.profile-menu').attr('class');
    if (className != undefined && className.includes('active')) {
        $('.profile-menu').removeClass('active');
    }
    if (className != undefined && !className.includes('active')) {
        $('.profile-menu').addClass('active');
    }
}

window.ShowToastr = (type, message) => {
    toastr.options = {
        "closeButton": true
    }
    if (type === "success") {
        toastr.success(message, "Success");
    }
    if (type === "error") {
        toastr.error(message, "Error", { timeOut: 2000 });
    }
}

function showLoaderBg() {
    var attr = $('.main-pages');
    if (attr != undefined) {
        $('.main-pages').css('background-image', 'url("images/auth-bg.jpg")');
        $('.main-pages').css('background-size', 'cover');
        $('.main-pages').css('background-repeat', 'no-repeat');
    }
}

function BaseActiveInactive(id, value) {
    if (id != "" && id != null) {
        document.getElementById(id).checked = value;
    }
}

function ShowErrorConfirmationModel() {
    $('#error-modal').modal('show');
}

window.getCoords = async () => {
    const pos = await new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(resolve, reject);
    });
    return [pos.coords.longitude, pos.coords.latitude];
};

function SetProductSubCategoryElemement(value) {
    if (value) {
        $("#ProductSubCategory").show();
    } else {
        $("#ProductSubCategory").hide();
    }
}

window.getCoords = async () => {
    try {
        const pos = await new Promise((resolve, reject) => {
            navigator.geolocation.getCurrentPosition(resolve, reject);
        });
        return [pos.coords.longitude, pos.coords.latitude];
    } catch (err) {
        return null;
    }
};

function scrollDown() {
    var height = 0;
    $('.chat-detail-section ul li').each(function (i, value) {
        height += parseInt($(this).height()) + 100;
    });

    height += '';

    $('.chat-section').animate({ scrollTop: height });
}

function MainMenuTabActive() {
    var activeTab;
    var currentUrl;
    activeTab = document.getElementsByClassName("nav-link");
    currentUrl = window.location.pathname.slice(1);
    if (currentUrl.includes("/")) {
        currentUrl = currentUrl.split('/')[0];
    }
    for (i = 0; i < activeTab.length; i++) {
        activeTab[i].className = activeTab[i].className.replace(" active", "");
    }
    if (currentUrl == null || currentUrl == "" || currentUrl == undefined) {
        $("#user").addClass('active');
    } else {
        $("#" + currentUrl).addClass('active');
    }
}

function check() {
    let inputs = document.getElementById('checkId');
    inputs.checked = true;
}
//create uncheck function 
function uncheck() {
    let inputs = document.getElementById('checkId');
    inputs.checked = false;
}
window.onload = function () {
    window.addEventListener('load', check, false);
}

function selectedInputFile() {
    var a = document.getElementById('file-upload').className;
    return a;
}