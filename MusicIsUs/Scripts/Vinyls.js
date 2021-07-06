$(".like").click(function () {
    let btn = this;
    $.post("/Vinyls/Like",
        {
            id: $(btn).data("vinyl-id"),
            like: $(btn).data("liked")
         },
        function () {
            var isTrueSet = ($(btn).data("liked").toLowerCase() === 'true');
            if (!isTrueSet) {
                $(btn).removeClass("btn-primary").addClass("btn-light");
                $(btn).data("liked", "True");
                $(btn).text("Liked");
            } else {
                $(btn).removeClass("btn-light").addClass("btn-primary");
                $(btn).data("liked", "False");
                $(btn).text("Like");
            }
    });
})