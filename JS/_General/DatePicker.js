
$(document).ready(function () {
    /***************** D A T E P I C  K E R****************/
    $('.datepicker').datepicker({
        showOn: "button",
        buttonImage: "Image/General/icon_calendar.png",
        buttonImageOnly: true,
        inline: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "dd/mm/yy",
        buttonText: "Elegir fecha",
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        nextText:"Siguiente",
        prevText:"Anterior"
    });

    $('.datepicker').focus(function () {
      $(this).datepicker("show");
    });
});