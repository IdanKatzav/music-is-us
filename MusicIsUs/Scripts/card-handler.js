$(document).ready(function () {

    $("#instLink").click(function () {
        if (!($("#instLink").hasClass("active"))) {
            $("#instLink").addClass("active");
            $("#vinLink").removeClass("active");
            $("#recommVinyls").addClass("hide");
            $("#recommInstruments").removeClass("hide");
        }
    });

    $("#vinLink").click(function () {
        if (!($("#vinLink").hasClass("active"))) {
            $("#vinLink").addClass("active");
            $("#instLink").removeClass("active");
            $("#recommInstruments").addClass("hide");
            $("#recommVinyls").removeClass("hide");
        }
    });
})

