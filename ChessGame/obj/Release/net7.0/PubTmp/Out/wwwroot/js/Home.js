var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .withHubProtocol(new signalR.JsonHubProtocol())
    .withAutomaticReconnect()
    .build();
connection.on("ReceiveData", function (data, Id) {
    // Hiển thị dữ liệu ngay lập tức khi nhận được
    document.getElementById(Id).textContent = data;
    if (Id == "user1" && data != "") {
        
        document.getElementById("pawn1").style.pointerEvents = "none";
        document.getElementById("loading1").style.display = "none";
        if (document.getElementById("user1").textContent != "" && document.getElementById("user2").textContent != "") {
            
            window.location.href = "Chess/Black?param1=" + encodeURIComponent(document.getElementById("user1").textContent) + "&param2=" + encodeURIComponent(document.getElementById("user2").textContent);
        }
    }
    else if (Id == "user2" && data != "") {
        document.getElementById("pawn2").style.pointerEvents = "none";
        document.getElementById("loading2").style.display = "none";
        if (document.getElementById("user1").textContent != "" && document.getElementById("user2").textContent != "") {
            
            window.location.href = "Chess/White?param1=" + encodeURIComponent(document.getElementById("user1").textContent) + "&param2=" + encodeURIComponent(document.getElementById("user2").textContent);
        }
    }
});
connection.on("ChangeA", function () {
    var dataInputs = document.querySelectorAll(".pawn");
    dataInputs.forEach(function (pawn) {
        if (pawn.id == "pawn1" && document.getElementById("user1").textContent == "") {
            document.getElementById("loading1").style.display = "block";
        } else if (pawn.id == "pawn2" && document.getElementById("user2").textContent == "") {
            document.getElementById("loading2").style.display = "block";
        }
            var data;
            var Id;

            if (pawn.id == "pawn1") {
                data = document.getElementById("user1").textContent;
                Id = "user1";
            } else if (pawn.id == "pawn2") {
                data = document.getElementById("user2").textContent;
                Id = "user2";
            }
            connection.invoke("SendData", data, Id).catch(function (err) {
                console.error(err.toString());
            });
    });
});
connection.start().catch(function (err) {
    console.error(err.toString());
});
var dataInputs = document.querySelectorAll(".pawn");
dataInputs.forEach(function (pawn) {
    pawn.addEventListener("click", async function () {
        var data;
        var Id;
        if (pawn.id == "pawn1") {
            document.getElementById("user1").textContent = document.getElementById("input").value;
            data = document.getElementById("input").value;
            document.getElementById("loading1").style.display = "none";
            Id = "user1";
            if (document.getElementById("user1").textContent != "" && document.getElementById("user2").textContent != "") {
                window.location.href = "Chess/White?param1=" + encodeURIComponent(document.getElementById("user1").textContent) + "&param2=" + encodeURIComponent(document.getElementById("user2").textContent);
            }
        } else if (pawn.id == "pawn2") {
            document.getElementById("user2").textContent = document.getElementById("input").value;
            data = document.getElementById("input").value;
            Id = "user2";
            document.getElementById("loading2").style.display = "none";
            if (document.getElementById("user1").textContent != "" && document.getElementById("user2").textContent != "") {
                window.location.href = "Chess/Black?param1=" + encodeURIComponent(document.getElementById("user1").textContent) + "&param2=" + encodeURIComponent(document.getElementById("user2").textContent);
            }
        }
        
        document.getElementById("pawn1").style.pointerEvents = "none";
        document.getElementById("pawn2").style.pointerEvents = "none";
        connection.invoke("SendData", data, Id).catch(function (err) {
            console.error(err.toString());
        });
    });
});

function Click() {
    var a = document.getElementById("input");
    var b = document.getElementById("Choose");
    var c = document.getElementById("username");
    if (a.value === "") {
        a.style.borderColor = "red";
    } else {
        c.style.display = "none";
        b.style.display = "flex";
    }
    connection.invoke("extendA").catch(function (err) {
        console.error(err.toString());
    });
}
var pawn1 = document.getElementById("pawn1");
var pawn2 = document.getElementById("pawn2");

pawn1.addEventListener("mouseover", function () {
    pawn1.style.transform = "scale(1.2)";
});
pawn1.addEventListener("mouseout", function () {
    pawn1.style.transform = "scale(1)";
});
pawn2.addEventListener("mouseover", function () {
    pawn2.style.transform = "scale(1.2)";
});
pawn2.addEventListener("mouseout", function () {
    pawn2.style.transform = "scale(1)";
});
