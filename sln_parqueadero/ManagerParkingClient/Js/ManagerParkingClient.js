var responseVehicles = {
    "async": true,
    "crossDomain": true,
    "url": "http://localhost:51123/api/vehicles/GetVehiclesAsync",
    "method": "GET"
}

var responseRecords = {
    "async": true,
    "crossDomain": true,
    "url": "http://localhost:51123/api/records/GetRecordsAsync",
    "method": "GET"
}

$(document).ready(function () {
    $("#divDisplacement").hide();
    $("#messageSuccess").hide();
    $("#messageError").hide();
    $("#txtDisplacement").keydown(function (e) {
        //Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl/cmd+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl/cmd+C
            (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl/cmd+X
            (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $("#txtDisplacement").attr('maxlength', '4');
    $("#txtPlate").attr('maxlength', '7');
    $("#txtBuscar").attr('maxlength', '7');

    getVehicles()
    getRecords();

    $('#vehicles').on('change', function () {
        $('#cells').empty();
        if ($("#vehicles option:selected").val() == 2) {
            $("#divDisplacement").slideDown("slow");
        } else {
            $("#divDisplacement").slideUp("slow");
        }
        getCellsByVehicleId(this.value)
    });
});

function getVehicles() {
    $.ajax(responseVehicles).done(function (response) {
        $.each(response, function (key, item) {
            $("#vehicles").append('<option value="' + item.Id + '">' + item.Description + '</option>')
        });
        var cellResult = $("#vehicles").val();
        console.log(cellResult);
        getCellsByVehicleId(cellResult)
    });
}

function getCellsByVehicleId(Id) {
    var datosJSON = {
        VehicleId: Id
    };

    $.ajax({
        type: 'GET',
        url: 'http://localhost:51123/api/cells/GetCellsVehicleIdAsync',
        data: datosJSON,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            $.each(response, function (key, item) {
                $("#cells").append('<option value="' + item.Id + '">' + item.CellNumber + '</option>')
            });
        },
        error: function (response) {
            console.log("Error " + response);
        }
    });
}

function mayus(e) {
    e.value = e.value.toUpperCase();
}

$("#btnBuscar").click(function () {
    var datosJSON = {
        Plate: $("#txtBuscar").val()
    };
    
    $.ajax({
        type: 'GET',
        url: 'http://localhost:51123/api/records/GetRecordPlateStateAsync',
        data: datosJSON,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (dato) {
            if (dato != null) {
                RegisterOutput(dato.Id, dato.Plate, dato.EntryDate, dato.CellNumber);
                $("#exampleModal").modal('show');
                $("#txtBuscar").val('');
            } else {
                $("#messageError").text("El vehiculo buscado no se encuentra activo en el parqueadero").fadeIn('slow').delay(3000).fadeOut('slow');
            }                       
        },
        error: function (dato) {
            $("#messageError").text("Error al guardar el registro").fadeIn('slow').delay(3000).fadeOut('slow');
        }
    });    
});

$("#btnGuardar").click(function () {
    if ($("#cells option:selected").val() == undefined) {
        $("#messageError").text("Debe seleccionar una celda disponible").fadeIn('slow').delay(3000).fadeOut('slow');
        return;
    }

    if ($("#vehicles option:selected").val() == 2) {
        if ($("#txtDisplacement").val() == "") {
            $("#messageError").text("Debe ingresar el cilindraje para motos").fadeIn('slow').delay(3000).fadeOut('slow');
            return;
        }
    }

    if ($("#txtPlate").val() == "") {
        $("#messageError").text("Debe ingresar la placa del vehículo").fadeIn('slow').delay(3000).fadeOut('slow');
        return;
    }

    $('#records tr').remove("tr:gt(0)");
    var datosJSON = {
        VehicleId: $("#vehicles option:selected").val(),
        CellId: $("#cells option:selected").val(),
        Displacement: $("#txtDisplacement").val(),
        Plate: $("#txtPlate").val()
    };
    $.ajax({
        type: 'POST',
        url: 'http://localhost:51123/api/records/RecordVehicleAsync',
        data: JSON.stringify(datosJSON),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (dato) {
            $("#txtDisplacement").val('');
            $("#txtPlate").val('');
            $("#cells").empty();
            if (dato == true) {
                $("#messageSuccess").text("Registro guardado con éxito").fadeIn('slow').delay(2000).fadeOut('slow');
            } else {
                $("#messageError").text("El vehículo se encuentra activo en el parqueadero").fadeIn('slow').delay(2000).fadeOut('slow');
            }
            getCellsByVehicleId($("#vehicles option:selected").val());
            getRecords();
        },
        error: function (dato) {
            $("#messageError").text("Error al guardar el registro").fadeIn('slow').delay(2000).fadeOut('slow');
        }
    });
});

function getRecords() {    
    $.ajax(responseRecords).done(function (response) {
        if (response != null) {
            $.each(response, function (key, item) {
                if (item.State == true) {
                    $('#records tr:last').after('<tr><td>' + item.VehicleDescription + '</td><td>' + item.Plate + '</td><td>' + item.CellNumber + '</td><td>' + item.EntryDate + '</td><td>' + "En el parqueadero" + '</td><td>' + item.PriceTimeParking + '</td><td>' + '<button onclick=RegisterOutput(' + item.Id + ',"' + item.Plate + '",' + '"' + item.EntryDate + '",' + '"' + item.CellNumber + '"' + ') data-toggle="modal" data-target="#exampleModal"><i class="fa fa-sign-out"></i></button>' + '</td></tr>');    
                }
                else {
                    $('#records tr:last').after('<tr><td>' + item.VehicleDescription + '</td><td>' + item.Plate + '</td><td>' + item.CellNumber + '</td><td>' + item.EntryDate + '</td><td>' + item.OutputDate + '</td><td>' + item.PriceTimeParking + '</td><td>' + '<p>Salió</p>' + '</td></tr>');                    
                }
            });
        }
    });
};

function RegisterOutput(Id, Plate, EntryDate, CellNumber) {
    $("#itemId").text(Id);
    $("#itemPlate").text(Plate);
    $("#itemEntryDate").text(EntryDate);
    $("#itemCellNumber").text(CellNumber);    
}

function outputVehicle() {
    $('#records tr').remove("tr:gt(0)");
    var datosJSON = {
        Id: $("#itemId").text()
    };

    console.log(datosJSON);

    $.ajax({
        type: 'POST',
        url: 'http://localhost:51123/api/records/OutputVehicleAsync/' + $("#itemId").text(),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (dato) {
            getRecords();
            $('#exampleModal').modal('toggle');
        },
        error: function (dato) {
        }
    });
}

