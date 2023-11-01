var yourtable="";
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .withHubProtocol(new signalR.JsonHubProtocol())
    .withAutomaticReconnect()
    .build();
connection.on("ReceiveData", function (data, Id,a) {
    // Hiển thị dữ liệu ngay lập tức khi nhận được
    document.getElementById(Id).textContent = data;
    if (Id.slice(4).length <3 && data != "") {
        
        document.getElementById("pawn" + Id.slice(4)).style.pointerEvents = "none";
        if (String(a) == yourtable && document.getElementById("user" + String(a)).textContent != "" && document.getElementById("user" + String(a) + "_" + String(a)).textContent != "") {
            window.location.href = "../Chess/Black?param1=" + encodeURIComponent(document.getElementById("user" + String(a)).textContent) + "&param2=" + encodeURIComponent(document.getElementById("user" + String(a) + "_" + String(a)).textContent) + "&scriptValue=" + a;
        }
    }
    else if (Id.slice(4).length >= 3 && data != "") {
        document.getElementById("pawn" + Id.slice(4)).style.pointerEvents = "none";
        if (String(a) == yourtable && document.getElementById("user" + String(a)).textContent != "" && document.getElementById("user" + String(a) + "_" + String(a)).textContent != "") {
            window.location.href = "../Chess/White?param1=" + encodeURIComponent(document.getElementById("user" + String(a)).textContent) + "&param2=" + encodeURIComponent(document.getElementById("user" + String(a) + "_" + String(a)).textContent) + "&scriptValue=" + a;
        }
    }
});
connection.on("ChangeA", function () {
    var dataInputs = document.querySelectorAll(".pawn");
    dataInputs.forEach(function (pawn,i) {
            var Id = "user" + pawn.id.slice(4);
            var data = document.getElementById(Id).textContent;
            connection.invoke("SendData","ReceiveData", data, Id,i+1).catch(function (err) {
                console.error(err.toString());
            });
    });
});
connection.start()
    .then(function () {
        console.log("Connection started.");
        return connection.invoke("extendA", "ChangeA");
    })
    .catch(function (err) {
        console.error(err.toString());
    });

var dataInputs = document.querySelectorAll(".pawn");
var t = 1;
var pattern = /pawn(.*?)_/;
dataInputs.forEach(function (pawn, i) {
    pawn.addEventListener("click", function () {
        let data;
        let Id;
        if (pawn.id.length == 5 || pawn.id.length == 6) {
            document.getElementById("user" + pawn.id.slice(4)).textContent = Username;
            data = Username;
            Id = "user" + pawn.id.slice(4);
            connection.invoke("SendData", "ReceiveData", data, Id, parseInt(pawn.id.slice(4))).catch(function (err) {
                console.error(err.toString());
            });
            if (document.getElementById("user" + pawn.id.slice(4)).textContent != "" && document.getElementById("user" + pawn.id.slice(4) + "_" + pawn.id.slice(4)).textContent != "") {
                window.location.href = "../Chess/White?param1=" + encodeURIComponent(document.getElementById("user" + pawn.id.slice(4)).textContent) + "&param2=" + encodeURIComponent(document.getElementById("user" + pawn.id.slice(4) + "_" + pawn.id.slice(4)).textContent) + "&scriptValue=" + pawn.id.slice(4);
            }
            var pawns = document.querySelectorAll(".pawn");
            pawns.forEach(function (pawn) {
                pawn.style.pointerEvents = "none";
            });
            yourtable = pawn.id.slice(4);
            
        }

        if (pawn.id.length > 6) {
            let result = pawn.id.slice(pawn.id.indexOf("n") + 1, pawn.id.indexOf("_"));
            data = Username;
            Id = "user" + pawn.id.slice(4);
            document.getElementById(Id).textContent = Username;
            connection.invoke("SendData", "ReceiveData", data, Id, parseInt(result)).catch(function (err) {
                console.error(err.toString());
            });
            if (document.getElementById("user" + result).textContent != "" && document.getElementById(Id).textContent != "") {
                window.location.href = "../Chess/Black?param1=" + encodeURIComponent(document.getElementById("user" + result).textContent) + "&param2=" + encodeURIComponent(document.getElementById("user" + pawn.id.slice(4)).textContent) + "&scriptValue=" + result;
            }
            var pawns = document.querySelectorAll(".pawn");
            pawns.forEach(function (pawn) {
                pawn.style.pointerEvents = "none";
            });
            yourtable = result;
            
        }

    });
});


