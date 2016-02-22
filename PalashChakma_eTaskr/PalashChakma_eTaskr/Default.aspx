<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PalashChakma_eTaskr.Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <meta charset="UTF-8" content="true" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.3/jquery-ui.min.js"></script>    
    <link href="Styles/weather.css" rel="stylesheet" />
    <title>Weather Forecast</title>    

    
    <script type="text/javascript">
       
        function getLocation() {            
            x = document.getElementById("weather");            
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);        
       
    } else { 
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

        function showPosition(position) {
            var lat, lng;
    //x.innerHTML = "Latitude: " + position.coords.latitude + 
    //"<br>Longitude: " + position.coords.longitude;
    lat = position.coords.latitude;
    lng = position.coords.longitude;

    CallWeather(lat,lng);
}

$(document).ready(function () {
    var x;    
    getLocation();
    
});


       
    </script>
    <script type="text/javascript">
        function CallWeather(lat,lng)
        {
            
            var jfetch = "";
            jfetch = JSON.stringify({ "latitude": lat, "longitude": lng }, null, 2);
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetWeather",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data:jfetch,
                async: "true",
                cache: "false",
                success: function (response) {
                    
                },
                Error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr.responseText);
                }
            });
            
        }
    </script>
    
</head>
<body>
    <div id="weather" />
    
    <form id="frmWeather" runat="server" class="frmstyle">
      
        <table>
            <tr>
                <td>
      
        <asp:Button ID="btnGetWeather" runat="server" OnClick="btnGetWeather_Click" Text="Show Weather" CssClass="btnstyle"/>
      
                </td>
                <td class="imgstyle">
                    <asp:Image ID="imgWeather" runat="server" AlternateText="image not found"/>
                </td>
            </tr>
            <tr>
                <td>Current Temperature</td>
                <td><asp:Label ID="lblcurrtemp" runat="server"></asp:Label></td>
            </tr>            
        </table>
      
    </form>
</body>
</html>