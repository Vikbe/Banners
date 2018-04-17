



//Encode Html and upload banner
$(document).ready(function () {
    $("#postform").submit(function (e) {
        e.preventDefault();
        var apiurl = "http://localhost:55786/api/Banner";

        var data = {
            Id: $("#postID").val(),
            Html: window.btoa($("#htmlInput").val())
        };
        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (d) {
                alert("Saved sucessfully!");
                document.getElementById("postform").reset();
            },
            error: function () {
                alert("Error please try again");
            }
        });
    });
});


// Get banner, decode html and render in browser
$(document).ready(function () {
    $("#getform").submit(function (e) {
        e.preventDefault();

        var apiurl = "http://localhost:55786/api/Banner";
        var id = $("#getID").val();

        var data = {
            Id: id
        };
        $.ajax({
            url: apiurl,
            type: 'GET',
            dataType: 'json',
            data: data,
            success: function (data) {
                content = window.atob(data.Html);
                $('#htmlbox').html(content); 

                // Views the raw html to be edited and updated
                $('#updateInput').text(content);
                $('#updateID').val(data.Id);
                  
            }, 
            error: function () {
                alert("Error could not retrieve requested banner");
            }
        });
    });
}); 


// Encode and send the updated html
$(document).ready(function () {
    $("#updateform").submit(function (e) {
        e.preventDefault();
        var apiurl = "http://localhost:55786/api/Banner";

        var data = {
            Id: $("#updateID").val(),
            Html: window.btoa($("#updateInput").val())
        };
        $.ajax({
            url: apiurl,
            type: 'PUT',
            dataType: 'json',
            data: data,
            success: function (d) {
                alert("Updated sucessfully!");
                
            },
            error: function () {
                alert("Error could not update banner.");
            }
        });
    });
}); 

$(document).ready(function () {
    $("#deleteform").submit(function (e) {
        e.preventDefault();

        var apiurl = "http://localhost:55786/api/Banner/";
        var id = $("#deleteID").val();
        $.ajax({
        url: apiurl + id,
        type: 'DELETE',
        success: function () {
            alert("Banner deleted!"); 
            document.getElementById("updateform").reset();

        },
        error: function () {
            alert("Error could not delete banner");
        }
        });
    });
}); 
