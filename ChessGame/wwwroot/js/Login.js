// Lấy tất cả các cookies
var allCookies = document.cookie;

// Chia tất cả các cookies thành một mảng các cặp key-value
var cookiesArray = allCookies.split('; ');

// Tạo một đối tượng để lưu trữ giá trị của cookie
var cookies = {};

// Lặp qua mảng các cookies và thêm chúng vào đối tượng cookies
for (var i = 0; i < cookiesArray.length; i++) {
    var cookie = cookiesArray[i].split('=');
    var cookieName = decodeURIComponent(cookie[0]);
    var cookieValue = decodeURIComponent(cookie[1]);
    cookies[cookieName] = cookieValue;
}

// Lấy giá trị của username và password từ cookie
var cookie_username = cookies['username'];
var cookie_password = cookies['password'];

// Kiểm tra xem username và password có tồn tại trong cookie
if (cookie_username && cookie_password) {
    $.ajax({
        url: "/Home/Check_Login",
        type: "GET",
        dataType: "json",
        success: function (data) {
            data.forEach(function (account) {
                console.log(document.cookie);
                if (account.username == cookie_username && account.pass == cookie_password) {
                    window.location.href = "../Home/Home?scriptValue=" + cookie_username;
                }
            });
        },
        error: function (xhr, status, error) {
            alert("Lỗi: " + error);
        }
    });
    
} else {
    console.log("Không tìm thấy username và password trong cookie.");
}

function Check_Login() {
    var Username = document.getElementById("Login_Username");
    var Password = document.getElementById("Login_Password");

    $.ajax({
        url: "/Home/Check_Login",
        type: "GET",
        dataType: "json",
        success: function (data) {
            if (data.length == 0) {
                alert("data null");
                return;
            }
            data.forEach(function (account) {
                console.log(account);
                if (account.username == Username.value && account.pass == Password.value) {
                    var expires = new Date();
                    expires.setDate(expires.getDate() + 30);
                    document.cookie = "username=" + Username.value + ";expires=" + expires.toUTCString();
                    document.cookie = "password=" + Password.value + ";expires=" + expires.toUTCString();
                    document.cookie = "email=" + account.email + ";expires=" + expires.toUTCString();
                    if (account.avatar == "") {
                        document.cookie = "avatar=" + "https://static.vecteezy.com/system/resources/thumbnails/008/442/086/small/illustration-of-human-icon-user-symbol-icon-modern-design-on-blank-background-free-vector.jpg" + ";expires=" + expires.toUTCString();
                    } else {
                        document.cookie = "avatar=" + account.avatar + ";expires=" + expires.toUTCString();
                    }
                    window.location.href = "../Home/Home?scriptValue=" + Username.value;
                } else {
                    Username.placeholder = "Incorrect Password";
                }
            });
            Username.placeholder = "Username does not exist";
        },
        error: function (xhr, status, error) {
            alert("Lỗi: " + error);
        }
    });

}

function Check_Signup() {
    var Username = document.getElementById("Signup_Username");
    var Password = document.getElementById("Signup_Password");
    var Confirm_Password = document.getElementById("Signup_Confirm_Password");
    var Email = document.getElementById("Signup_Email");
    
    if (Password.value != Confirm_Password.value) {
        alert("The passwords must be the same");
        return;
    }
    if (!Username.value && !Password.value && !Email.value) {
        alert("nullll");
        return;
    }
   
    $.ajax({
        url: "/Home/Check_Login",
        type: "GET",
        dataType: "json",
        success: function (data) {
            if (data.length === 0) {
                // insert
                $.ajax({
                    url: "/Home/Check_Signup",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({ Username: Username.value, Pass: Password.value, Email: Email.value, Avatar: "", Win: 0, Lost: 0 }),
                    success: function (result) {
                        if (result.success) {
                            window.location.href = "../Home/Login";
                        } else {
                            alert(result.message);
                        }
                    },
                    error: function (xhr, status, error) {
                        alert("Có lỗi xảy ra: " + error);
                    }
                });
                // end insert
            } else {
                var usernameExists = false;
                data.forEach(function (item) {
                    if (item.Username === Username.value) {
                        Username.placeholder = "Username is already in use";
                        usernameExists = true;
                    }
                });

                if (!usernameExists) {
                    // insert
                    $.ajax({
                        url: "/Home/Check_Signup",
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify({ Username: Username.value, Pass: Password.value, Email: Email.value, Win: 0, Lost: 0 }),
                        success: function (result) {
                            if (result.success) {
                                window.location.href = "../Home/Login";
                            } else {
                                alert(result.message);
                            }
                        },
                        error: function (xhr, status, error) {
                            alert("Có lỗi xảy ra: " + error);
                        }
                    });
                    // end insert
                }
            }
        },
        error: function (xhr, status, error) {
            alert("Thất bại");
        }
    });

}
