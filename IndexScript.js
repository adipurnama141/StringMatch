$(document).ready(function () {
    $("#key1").val("Bocor,  ngalir, tersumbat, mengalir,ngocor, air , kotor , saluran,lancar, aliran ,  pipa, limbah");
    $("#key2").val("perbaikan,pju,pohon");
    $("#key3").val("macet,ngetem,angkot,lalu lintas,lalin,jalan,perlintasan");
    $("#key4").val("loker, lowongan");
    $("#key5").val("persib, PON");
    $("#searchMethod").val("KMP");
    $(".settings_page").hide();

    $(".tweetcontainer").hide();
    $(".maps").hide();
    $(".info").hide();
    $(".1").show();
    var isSearched = 0;

    var i = 0;
    $(".settings_icon").click(function () {
        if (i == 0) {
            $(".landingpage").hide();
            $(".search_page").fadeOut().delay(300);
            $(".settings_page").fadeIn();
            i++;
        }
        else {
            $(".settings_page").fadeOut();
            if (isSearched == 0) {
                $(".landingpage").fadeIn().delay(300);
            }
            else {
                $(".search_page").fadeIn().delay(300);
            }
            
            i = 0;
        }
    });

    $(".m1").click(function () {
        $(".tweetcontainer").hide();
        $(".maps").hide();
        $(".info").hide();
        $(".1").fadeIn();
    });
    $(".m2").click(function () {
        $(".tweetcontainer").hide();
        $(".maps").hide();
        $(".info").hide();
        $(".2").fadeIn();
    });
    $(".m3").click(function () {
        $(".tweetcontainer").hide();
        $(".maps").hide();
        $(".info").hide();
        $(".3").fadeIn();
    });
    $(".m4").click(function () {
        $(".tweetcontainer").hide();
        $(".maps").hide();
        $(".info").hide();
        $(".4").fadeIn();
    });
    $(".m5").click(function () {
        $(".tweetcontainer").hide();
        $(".maps").hide();
        $(".info").hide();
        $(".5").fadeIn();
    });
    $(".m6").click(function () {
        $(".tweetcontainer").hide();
        $(".maps").hide();
        $(".info").hide();
        $(".0").fadeIn();
    });
    $(".m7").click(function () {
        $(".tweetcontainer").fadeIn();
        $(".maps").fadeIn();
        $(".info").hide();

    });

    $(".button").click(function () {
        $(".input_form").focus();

    });


    var ix = 1;
    $(".menu_search").click(function () {
        if (ix == 1) {
            $(this).html("Metode Pencarian : Boyer Moore");
            $("#searchMethod").val("BM");
            ix = 2;
        }
        else {
            $(this).html("Metode Pencarian : Knuth Morris Pratt");
            $("#searchMethod").val("BM");
            ix = 1;
        }
    });

    $(".menu").click(function () {
        $(".menu").removeClass("menu_active");
        $(this).addClass("menu_active");
    });

});