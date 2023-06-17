<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Establecimiento.aspx.cs" Inherits="Proyecto.Vistas.Establecimiento" %>

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

        form{
            border: 1px solid #000;
        }

        hr{
            border: 1px solid #000;
        }

        
    </style>
    <title>ESTABLECIMIENTOS</title>
</head>

    <script type="text/javascript">

        $(document).ready(function () {
            getHora();
            getAllEstabllecimientos();

            $('#btnGethora').click(function () {
                getHora();
            });

            $('#btnInsEsta').click(function () {
                insertaEsta();
                getAllEstabllecimientos();
            });

            $('#btnEditar').click(function () {
                updateEsta();
                getAllEstabllecimientos();
            });

            $('#btnBorrar').click(function () {
                deleteEsta();
                getAllEstabllecimientos();
            });
        });

        //obtiene la hora
        function getHora() {
            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSEstablecimientos.asmx/getDate",
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
        function insertaEsta() {
            var nombre = $('#nombre').val();
            var direccion = $('#direccion').val();
            var telefono = $('#telefono').val();
            var fax = $('#fax').val();

            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSEstablecimientos.asmx/InsertEsta",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{'NOMBRE':'" + nombre + "','DIRECCION':'" + direccion + "','TELEFONO':'" + telefono + "','FAX':'" + fax + "'}",
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    if (items == "SUCCESS") {
                        alert('Insertado correctamente');
                        getHora();
                        getAllPaises();
                    } else {
                        alert('Validar');
                        console.log(items);
                    }
                },
            });
        }

        //Update Paises
        function updateEsta() {
            var idEsta = $('#IdEsta').val();
            var nombre = $('#txtNombre').val();
            var direccion = $('#txtDireccion').val();
            var telefono = $('#txtTelefono').val();
            var fax = $('#txtFax').val();

            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSEstablecimientos.asmx/updateEsta",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{'ID_ESTABLECIMIENTO':'" + idEsta + "','NOMBRE':'" + nombre + "','DIRECCION':'" + direccion + "','TELEFONO':'" + telefono + "','FAX':'" + fax + "'}",
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    if (items == "SUCCESS") {
                        alert('Actualizado correctamente');
                        getHora();
                    } else {
                        alert('Validar');
                        console.log(items);
                    }
                },
            });
        }

        //delete
        function deleteEsta() {
            var idEsta = $('#IdEsta').val();

            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSEstablecimientos.asmx/DeleteEsta",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{'ID_ESTABLECIMIENTO':'" + idEsta + "'}",
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    if (items == "SUCCESS") {
                        alert('Borrado correctamente');
                        getHora();
                    } else {
                        alert('Validar');
                        console.log(items);
                    }
                },
            });
        }

        //Mostrar todos los establecimientos
        function getAllEstabllecimientos() {
            $.ajax({
                type: 'POST',
                url: "https://localhost:44395/Servicios/WSEstablecimientos.asmx/GetAllEstablecimientos",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (resultado) {
                    console.log(resultado)
                    var items = resultado.d;
                    $('.claseTabla').html('');
                    $.each(items, function (index, item) {
                        $('.claseTabla').append("<tr><th scope='row'>" + item.ID_ESTABLECIMIENTO + "</th><td>" + item.NOMBRE + "</td><td>" + item.DIRECCION + "</td><td>" + item.TELEFONO + "</td><td>" + item.FAX + "</td>");
                    });
                },
            });
        }


    </script>
<body>
     <form id="form1" runat="server">
        <h1 class ="text-center">Sistema de Administración de Establecimientos</h1>
        <hr />
          <h2 class ="text-center">Fecha y Hora</h2>
        <div class="grid text center container">
            <table class="table-secondary table">
                <tr>
                    <td>
                        <label id="lblHora" class="form-control">Fecha y Hora</label>            
                    </td>
                    <td>
                        <input id="horaFecha" type="text" class="form-control" readonly="true" placeholder="Fecha y Hora" aria-label="Hora y fecha" />            
                    </td>
                    <td>
                        <button id="btnGethora" type="button" class="btn btn-primary" style="width:auto;">Obtener Hora</button>
                    </td>
                </tr>
            </table>
        </div>

         <h2 class ="text-center">Agegar Establecimiento</h2>
        <div class="grid text center container">
        <table class="table-primary table">
                <thead>
                    <tr>
                        <th scope="col">Nombre</th>
                        <th scope="col">Dirección</th>
                        <th scope="col">Telefono</th>
                        <th scope="col">Fax</th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <input id="nombre" type="text" class="form-control" placeholder="Nombre" aria-label="Nombre" />
                        </td>
                        <td>
                            <input id="direccion" type="text" class="form-control" placeholder="Dirección" aria-label="Dirección" />
                        </td>
                        <td>
                            <input id="telefono" type="text" class="form-control" placeholder="Teléfono" aria-label="Teléfono" />
                        </td>
                        <td>
                            <input id="fax" type="text" class="form-control" placeholder="FAX" aria-label="Fax" />
                        </td>
                        <td>
                            <button id="btnInsEsta" type="button" class="btn btn-success" style="width: auto;">Guardar</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        
        </div>

         <h2 class ="text-center">Editar / Borrar</h2>
        <div class="grid text center container">
        <table class="table-primary table">
                <thead>
                    <tr>
                        <th scope="col">ID Establecimiento</th>
                        <th scope="col">Nombre</th>
                        <th scope="col">Direccion</th>
                        <th scope="col">Telefono</th>
                        <th scope="col">Fax</th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <input id="IdEsta" type="text" class="form-control" placeholder="ID Establecimiento" aria-label="ID Establecimiento" />
                        </td>
                        <td>
                            <input id="txtNombre" type="text" class="form-control" placeholder="Nombre" aria-label="Nombre" />
                        </td>
                        <td>
                            <input id="txtDireccion" type="text" class="form-control" placeholder="Dirección" aria-label="Dirección" />
                        </td>
                        <td>
                            <input id="txtTelefono" type="text" class="form-control" placeholder="Teléfono" aria-label="Teléfono" />
                        </td>

                        <td>
                            <input id="txtFax" type="text" class="form-control" placeholder="Fax" aria-label="Fax" />
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
                    class="claseTabla" style="text-align-last: center;">
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
