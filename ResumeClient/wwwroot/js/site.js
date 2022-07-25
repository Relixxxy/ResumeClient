$(function () {
    function fadeOutAll() {
        $('.editProfile_form').fadeOut();
        $('.deleteProlife').fadeOut();
        $('.addMainSkill_form').fadeOut();
        $('.addAdditionSkill_form').fadeOut();
        $('.addSection_form').fadeOut();
        $('.addProject_form').fadeOut();
    }

    fadeOutAll();
     

    $('.editProfile_btn').click(function (e) {
        fadeOutAll();
        $('.editProfile_form').fadeIn();
    });

    $('.deleteProlife_btn').click(function (e) {
        fadeOutAll();
        $('.deleteProlife').fadeIn();
    }); 

    $('.addMainSkill_btn').click(function (e) {
        fadeOutAll();
        $('.addMainSkill_form').fadeIn();
    });

    $('.addAdditionSkill_btn').click(function (e) {
        fadeOutAll();
        $('.addAdditionSkill_form').fadeIn();
    });

    $('.addSection_btn').click(function (e) {
        fadeOutAll();
        $('.addSection_form').fadeIn();
    });

    $('.addProject_btn ').click(function (e) {
        fadeOutAll();
        $('.addProject_form').fadeIn();
    });

    $('.open-popup').click(function (e) {
        e.preventDefault();
        $('.popup-bg').fadeIn(600);
    });
    $('.close-popup').click(function () {
        $('.popup-bg').fadeOut(600);
    });

    $("#add_img_inp").on({
        "change": function () {
            var file = $(this).prop("files")[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                $("#add_img_lbl").css("background-image", 'url(' + reader.result + ')');
            }

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    });

    $("#add_img_inp_project").on({
        "change": function () {
            var file = $(this).prop("files")[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                $("#add_img_lbl_project").css("background-image", 'url(' + reader.result + ')');
            }

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    });
});