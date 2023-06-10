$(document).ready(function () {



    $(document).on("click", ".status-slider", function () {
   

        let sliderId = $(this).attr("data-id");

        let changedElem = $(this);

        let data = { id: sliderId };




        $ajax({

            url: "slider/changestatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    $(cjangedElem).removeClass("status-false");
                    $(cjangedElem).addClass("status-true")
                }
                else {
                    $(cjangedElem).removeClass("status-true");
                    $(cjangedElem).addClass("status-false")
                }
            }
        })




    })
})