function CustomToggleBar() {
    $(".custom-main-section, header").toggleClass("move-to-left"), $(".custom-toggle-bar").toggleClass("toggle-close-bar"), $(".modal-backdrop").toggleClass("custom-backdrop");
}
//function categorySwiper() {
//    new Swiper(".categorySwiper", {
//        slidesPerView: 6,
//        loop: false,
//        infinite: !0,
//        speed: 1e3,
//        autoplay: { delay: 2e3 },
//        grid: { rows: 1 },
//        navigation: { nextEl: ".swiper-button-next", prevEl: ".swiper-button-prev" },
//        breakpoints: { 280: { slidesPerView: 3, spaceBetween: 0, grid: { rows: 2 } }, 576: { slidesPerView: 4, spaceBetween: 16 }, 768: { slidesPerView: 5, spaceBetween: 18 }, 992: { slidesPerView: 6, spaceBetween: 20 } },
//    });
//}
function Remove() {
    console.log("click");
}
function StoreMultiImages() {
    var e = new Swiper(".thumbnailsSwiper", { spaceBetween: 10, slidesPerView: 7, freeMode: !0, watchSlidesProgress: !0, breakpoints: { 280: { slidesPerView: 5 }, 576: { slidesPerView: 7 } } });
    new Swiper(".productSwiper", { spaceBetween: 10, thumbs: { swiper: e } });
}
$(".file-input").change(function () {
    var e = $(".editimage");
    console.log(e);
    var s = new FileReader();
    (s.onload = function (s) {
        e.attr("src", s.target.result);
    }),
        s.readAsDataURL(this.files[0]);
});