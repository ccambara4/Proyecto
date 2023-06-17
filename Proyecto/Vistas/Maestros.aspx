<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Maestros.aspx.cs" Inherits="Proyecto.Vistas.Maestros" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.7.0/jquery-3.7.0.js"></script>
    <script src="../Scripts/bootstrap/js/bootstrap.min.js"></script>
    <style>
        body {
            background-color: #f1edda;
        }

        table {
            border: 1px solid #000;
        }
        td{
            border: 1px solid #000;
        }

        tr{
            border: 1px solid #000;
        }

        th{
            border: 1px solid #000;
        }

        hr{
            border: 1px solid #000;
        }
    </style>
    <title>MAESTROS</title>
</head>
    <script type="text/javascript">

        $(document).ready(function () {
            getHora();
            getAllMaes();
            getAllEsta();

            $('#btnGethora').click(function () {
                getHora();
            });

            $('#btnInsMaes').click(function () {
                insertaMaes();
                getAllMaes();
            });

            $('#btnEditar').click(function () {
                updateMaes();
                getAllMaes();
            });

            $('#btnBorrar').click(function () {
                deleteMaes();
                getAllMaes();
            });
        });

        //obtiene la hora
        function getHora() {
            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSMaestros.asmx/getDate",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    $('#horaFecha').val(items);
                },
            });
        }

        //Insertar
        function insertaMaes() {
            var dpi = $('#DPI').val();
            var nombre = $('#NOMBRE').val();
            var apellido = $('#APELLIDO').val();
            var direccion = $('#DIRECCION').val();
            var edad = $('#EDAD').val();
            var telefono = $('#TELEFONO').val();
            var idEsta = $('#IdEsta').val();

            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSMaestros.asmx/InsertMaes",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{'DPI':'" + dpi + "','NOMBRE':'" + nombre + "','APELLIDO':'" + apellido + "','DIRECCION':'" + direccion + "','EDAD':'" + edad + "','TELEFONO':'" + telefono + "','ID_ESTABLECIMIENTO':'"+ idEsta +"'}",
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    if (items == "SUCCESS") {
                        alert('Insertado correctamente');
                        getHora();
                        getAllMaes();
                    } else {
                        alert('Validar');
                        console.log(items);
                    }
                },
            });
        }

        //Update Paises
        function updateMaes() {
            var idMaes = $('#txtIdMaes').val();
            var dpi = $('#txtDPI').val();
            var nombre = $('#txtNOMBRE').val();
            var apellido = $('#txtAPELLIDO').val();
            var direccion = $('#txtDIRECCION').val();
            var edad = $('#txtEDAD').val();
            var telefono = $('#txtTELEFONO').val();
            var idEsta = $('#txtIdEsta').val();

            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSMaestros.asmx/updateMaes",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{'ID_MAESTRO':'" + idMaes + "','DPI':'" + dpi + "','NOMBRE':'" + nombre + "','APELLIDO':'" + apellido + "','DIRECCION':'" + direccion+ "','EDAD':'" + edad + "','TELEFONO':'" + telefono + "','ID_ESTABLECIMIENTO':'" + idEsta +"'}",
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    if (items == "SUCCESS") {
                        alert('Actualizado correctamente');
                        getHora();
                        getAllMaes();
                    } else {
                        alert('Validar');
                        console.log(items);
                    }
                },
            });
        }

        //delete
        function deleteMaes() {
            var idMaes = $('#txtIdMaes').val();

            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSMaestros.asmx/DeleteMaes",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{'ID_MAESTRO':'" + idMaes + "'}",
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    if (items == "SUCCESS") {
                        alert('Borrado correctamente');
                        getHora();
                        getAllMaes();
                    } else {
                        alert('Validar');
                        console.log(items);
                    }
                },
            });
        }

        //Mostrar todos los establecimientos
        function getAllMaes() {
            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSMaestros.asmx/GetAllMaestros",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    $('.claseTabla').html('');
                    $.each(items, function (index, item) {
                        $('.claseTabla').append("<tr><th scope='row'>" + item.ID_MAESTRO + "</th><td>" + item.DPI + "</td><td>" + item.NOMBRE + "</td><td>" + item.APELLIDO + "</td><td>" + item.DIRECCION + "</td><td>" + item.EDAD + "</td><td>" + item.TELEFONO + "</td><td>" + item.ID_ESTABLECIMIENTO + "</td><td>" + item.ESTABLECIMIENTO + "</td>");
                    });
                },
            });
        }

        //Mostrar todos los establecimientos
        function getAllEsta() {
            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSEstablecimientos.asmx/GetAllEstablecimientos",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    $('.claseTabla1').html('');
                    $.each(items, function (index, item) {
                        $('.claseTabla1').append("<tr><th scope='row'>" + item.ID_ESTABLECIMIENTO + "</th><td>" + item.NOMBRE + "</td><td>" + item.DIRECCION + "</td><td>" + item.TELEFONO + "</td><td>" + item.FAX + "</td>");
                    });
                },
            });
        }


    </script>
<body>
    <form id="form1" runat="server">
        <h1 class ="text-center">Sistema de Administración de Maestros</h1>
        <hr />
          <h2 class ="text-center">Fecha y Hora</h2>
        <div class="grid text center container">
            <table class="table-secondary table">
                <tr>
                    
                    <td>
                        <input id="horaFecha" type="text" class="form-control" readonly="true" placeholder="Fecha y Hora" aria-label="Hora y fecha" />            
                    </td>
                    <td>
                        <button id="btnGethora" type="button" class="btn btn-primary" style="width:auto;">Obtener Hora</button>
                    </td>
                </tr>
            </table>
        </div>

        <h2 class ="text-center">Agegar Maestro</h2>
        <div class="grid text center container">
        <table class="table-primary table">
                <thead>
                    <tr>
                        <th scope="col">DPI</th>
                        <th scope="col">NOMBRE</th>
                        <th scope="col">APELLIDO</th>
                        <th scope="col">DIRECCIÓN</th>
                        <th scope="col">EDAD</th>
                        <th scope="col">TELEFONO</th>
                        <th scope="col">ID_ESTABLECIMIENTO</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <input id="DPI" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="NOMBRE" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="APELLIDO" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="DIRECCION" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="EDAD" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="TELEFONO" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="IdEsta" type="text" class="form-control"/>
                        </td>
                        <td>
                            <button id="btnInsMaes" type="button" class="btn btn-success" style="width: auto;">Guardar</button>
                        </td>
                    </tr>
                </tbody>
            </table>

            <h2 class ="text-center">Editar / Borrar</h2>
        <div class="grid text center container">
        <table class="table-secondary table">
                <thead>
                    <tr>
                        <th scope="col">ID_MAESTRO</th>
                        <th scope="col">DPI</th>
                        <th scope="col">NOMBRE</th>
                        <th scope="col">APELLIDO</th>
                        <th scope="col">DIRECCIÓN</th>
                        <th scope="col">EDAD</th>
                        <th scope="col">TELEFONO</th>
                        <th scope="col">ID_ESTABLECIMIENTO</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <input id="txtIdMaes" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="txtDPI" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtNOMBRE" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtAPELLIDO" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtDIRECCION" type="text" class="form-control" />
                        </td>
                        <td>
                            <input id="txtEDAD" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtTELEFONO" type="text" class="form-control"/>
                        </td>
                        <td>
                            <input id="txtIdEsta" type="text" class="form-control" />
                        </td>
                        <td>
                            <button id="btnEditar" type="button" class="btn btn-warning" style="width: auto;">Editar</button>
                            </td>
                        <td>
                            <button id="btnBorrar" type="button" class="btn btn-danger" style="width: auto;">Borrar</button>
                        </td>
                    </tr>
                </tbody>
            </table>        
        </div>
        
        </div>
         <h2 class ="text-center">Maestros</h2>
          <div class="grid text-center container">
            <table class="table-primary table table-striped">
                <thead>
                    <tr>
                        <th scope="col">ID_MAESTRO</th>
                        <th scope="col">DPI</th>
                        <th scope="col">NOMBRE</th>
                        <th scope="col">APELLIDO</th>
                        <th scope="col">DIRECCIÓN</th>
                        <th scope="col">EDAD</th>
                        <th scope="col">TELEFONO</th>
                        <th scope="col">ID_ESTABLECIMIENTO</th>
                        <th scope="col">ESTABLECIMIENTO</th>
                    </tr>
                </thead>
                <tbody
                    class="claseTabla" style="text-align-last: center;">
                </tbody>
            </table>
        </div>

        <h2 class ="text-center">Establecimientos</h2>
          <div class="grid text-center container">
            <table class="table-secondary table table-striped">
                <thead>
                    <tr>
                        <th scope="col">ID_ESTABLECIMIENTO</th>
                        <th scope="col">NOMBRE</th>
                        <th scope="col">DIRECCIÓN</th>
                        <th scope="col">TELÉFONO</th>
                        <th scope="col">FAX</th>
                    </tr>
                </thead>
                <tbody
                    class="claseTabla1" style="text-align-last: center;">
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
