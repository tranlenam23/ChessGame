var leaderboardTable = document.getElementById("leaderboard");
var rank = 1;
$.ajax({
    url: "/Home/TopRank",
    method: "GET",
    dataType: "json",
    success: function (data) {
        if (data.length == 0) {
            alert("data null");
            return;
        }
        data.forEach(account => {
            var row = leaderboardTable.insertRow();
            var rankCell = row.insertCell(0);
            var imageCell = row.insertCell(1);
            var nameCell = row.insertCell(2);
            var scoreCell = row.insertCell(3);

            if (rank <= 3) {
                // Nếu rank là 1, 2, hoặc 3, sử dụng hình ảnh tương ứng
                var rankImage = document.createElement("img");
                rankImage.src = "../img/" + rank + ".png";
                rankImage.className = "rank-image";
                rankCell.appendChild(rankImage);
            } else {
                rankCell.innerHTML = rank;
            }
            rank++;

            var image = document.createElement("img");
            image.src = account.avatar;
            image.className = "round-image"; // Thêm lớp "round-image" để làm hình ảnh thành hình tròn
            imageCell.appendChild(image);

            nameCell.innerHTML = account.username;
            scoreCell.innerHTML = account.win - account.lost;
        });
    },
    error: function (error) {
        alert("Lỗi: " + error);
    }
});


