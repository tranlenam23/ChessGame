// Toggle password fields when "Change Password" button is clicked
document.getElementById("changePassword").addEventListener("click", function () {
    var passwordFields = document.getElementById("passwordFields");
    if (passwordFields.style.display === "none" || passwordFields.style.display === "") {
        passwordFields.style.display = "block";
    } else {
        passwordFields.style.display = "none";
    }
});

var allCookies = document.cookie;
var cookiesArray = allCookies.split('; ');
var cookies = {};
for (var i = 0; i < cookiesArray.length; i++) {
    var cookie = cookiesArray[i].split('=');
    var cookieName = decodeURIComponent(cookie[0]);
    var cookieValue = decodeURIComponent(cookie[1]);
    cookies[cookieName] = cookieValue;
}

var cookie_username = cookies['username'];
var cookie_email = cookies['email'];
var cookie_avatar = cookies['avatar'];
document.getElementById("username").value = cookie_username;
document.getElementById("email").value = cookie_email;
document.getElementById('profileImage').src = cookie_avatar;

function loadImage() {
    var input = document.getElementById('avatar');
    var image = document.getElementById('profileImage');
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            image.src = e.target.result;
        };
        reader.readAsDataURL(input.files[0]);
    }
}


$.ajax({
    url: "/Home/Check_Login",
    type: "GET",
    dataType: "json",
    success: function (data) {
        data.forEach(function (account) {
            if (account.username == cookie_username) {
                document.getElementById("winsCount").textContent = account.win;
                document.getElementById("lossesCount").textContent = account.lost;

            }
        });
    },
    error: function (xhr, status, error) {
        alert("Lỗi: " + error);
    }
});