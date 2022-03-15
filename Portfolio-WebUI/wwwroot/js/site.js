// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let menu = $(".right-menu");

$(".menu-btn").click(function (e) {
    e.preventDefault();

    if (menu.css("right") != 0)
        menu.css("right", "0");

});
$(".close-btn").click(function (e) {
    e.preventDefault();
    menu.css("right", "-120%");
      
})


