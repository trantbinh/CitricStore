$(document).on('ready', () => {

    $("#files").on('change', function () {

        $(".filearray").empty();

        for (let i = 0; i < this.files.length; ++i) {

            let filereader = new FileReader();

            let $img = jQuery.parseHTML("<img width='150' height='100' src=''>");

            filereader.onload = function () {

                $img[0].src = this.result;

            };

            filereader.readAsDataURL(this.files[i]);

            $(".filearray").append($img);

        }

    });

});