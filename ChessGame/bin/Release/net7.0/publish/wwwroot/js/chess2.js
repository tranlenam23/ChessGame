var x = [];
for (let i = 1; i <= 8; i++) {
    x[i] = new Array();
    x[i][9] = 0;
}


window.addEventListener('beforeunload', function (e) {
    var parts1 = time1.value.split(":");
    UpDate(3, 9, (parseInt(parts1[0]) * 100 + (parseInt(parts1[1]))));
    var parts2 = time2.value.split(":");
    UpDate(4, 9, (parseInt(parts2[0]) * 100 + (parseInt(parts2[1]))));

    var confirmationMessage = 'Bạn có chắc chắn muốn thoát khỏi trang này?';
    (e || window.event).returnValue = confirmationMessage;
    return confirmationMessage;
});

var a = 1;
for (let i = 1; i <= 8; i++) {
    for (let j = 1; j <= 8; j++) {
        x[i][j] = 0;
        let t = document.getElementById(String(i + ',' + j));
        t.classList = [];
        t.classList.add("node");
        t.addEventListener("dragover", function (e) {
            e.preventDefault();
        });
        if (a == 1) {
            if (j % 2 == 0) {
                t.classList.add("black");
            } else {
                t.classList.add("white");
            }
        } else {
            if (j % 2 == 0) {
                t.classList.add("white");
            } else {
                t.classList.add("black");
            }
        }
    }
    a = -a;
}

var b = 1;


function countdown() {
    if (minute1 == 0 && second1 == 0 && mili1 == 0) {
        clearInterval(timerun2);
        alert("Đen Thắng!!!");
    }
    if (minute2 == 0 && second2 == 0 && mili2 == 0) {
        clearInterval(timerun2);
        alert("Trắng Thắng!!!");
    }
    if (mili1 < 0 && second1 == 0) {
        minute1--;
    }
    if (second1 < 0) {
        second1 = 59;
    }
    if (mili1 < 0) {
        second1--;
    }
    if (mili1 < 0) {
        mili1 = 9;
    }
    if (mili2 < 0 && second2 == 0) {
        minute2--;
    }
    if (second2 < 0) {
        second2 = 59;
    }
    if (mili2 < 0) {
        second2--;
    }
    if (mili2 < 0) {
        mili2 = 9;
    }
    if (second1 >= 10) {
        time1.value = String(minute1 + ':' + second1 + ':' + mili1);
    } else if (second1 < 10) {
        time1.value = String(minute1 + ':0' + second1 + ':' + mili1);
    }
    if (second2 >= 10) {
        time2.value = String(minute2 + ':' + second2 + ':' + mili2);
    } else if (second2 < 10) {
        time2.value = String(minute2 + ':0' + second2 + ':' + mili2);
    }
    // second2-=1;
    if (luot == 1) {
        mili1--;
    } else if (luot == 0) {
        mili2--;
    }
}

function NewGame() {
    fetch("/Chess/NewGame", {
        method: 'POST', // hoặc 'GET' tùy thuộc vào cách bạn đã cấu hình controller của bạn
        headers: {
            'Content-Type': 'application/json' // Điều này phụ thuộc vào cách bạn cấu hình controller của bạn
        }
    })
        .then(response => response.json())
        .catch(error => {
            console.error('Lỗi:', error);
        });
    luot = 1;
    minute1 = 10;
    minute2 = 10;
    second1 = 0;
    second2 = 0;
    clearInterval(timerun2);
    

    fetch("/Chess/GetData")
        .then(response => response.json())
        .then(data => {
            data.forEach(item => {
                x[item.x][item.y] = item.value;
            })
            for (let i = 1; i <= 8; i++) {
                for (let j = 1; j <= 8; j++) {
                    let t = document.getElementById(String(i + ',' + j));
                    t.classList.remove("tot_den", "tot_trang", "xe_den", "xe_trang", "ma_den", "ma_trang", "tuong_den", "tuong_trang", "hau_den", "hau_trang", "vua_den", "vua_trang");
                    switch (x[i][j]) {
                        case 1: t.classList.add("tot_den");
                            break;
                        case 5: t.classList.add("xe_den");
                            break;
                        case 2: t.classList.add("ma_den");
                            break;
                        case 3: t.classList.add("tuong_den");
                            break;
                        case 9: t.classList.add("hau_den");
                            break;
                        case 10: t.classList.add("vua_den");
                            break;
                        case 11: t.classList.add("tot_trang");
                            break;
                        case 15: t.classList.add("xe_trang");
                            break;
                        case 12: t.classList.add("ma_trang");
                            break;
                        case 13: t.classList.add("tuong_trang");
                            break;
                        case 19: t.classList.add("hau_trang");
                            break;
                        case 20: t.classList.add("vua_trang");
                            break;
                    }

                }
            }
            timerun2 = setInterval(() => {
                countdown();
            }, 100);
        })
        .catch(error => alert("that bai"));
}


var time1 = document.getElementById("clock1");


var minute1;
var second1;
var mili1 = 0;
var mili2 = 0;
var time2 = document.getElementById("clock2");
var minute2;
var second2;

let TimeNewGame = setInterval(() => {
    fetch("/Chess/GetData")
        .then(response => response.json())
        .then(data => {
            data.forEach(item => {
                x[item.x][item.y] = item.value;
            })
            if (x[3][9] == x[4][9] && x[3][9] == 1000) {
                second1 = 0;
                minute1 = 10;
                second2 = 0;
                minute2 = 10;
                clearInterval(TimeNewGame);
            }



        })
        .catch(error => alert("that bai"));
},300);

// Kiểm tra giá trị và xử lý nếu cần
let Start = setInterval(() => {
    let web1IsOpen = localStorage.getItem('web1IsOpen');
    let web2IsOpen = localStorage.getItem('web2IsOpen');
    if (web1IsOpen == 'true' && web2IsOpen == 'true') {
        fetch("/Chess/GetData") 
            .then(response => response.json())
            .then(data => {
                data.forEach(item => {
                    x[item.x][item.y] = item.value;
                })
                luot = x[1][9];
                for (let i = 1; i <= 8; i++) {
                    for (let j = 1; j <= 8; j++) {
                        let t = document.getElementById(String(i + ',' + j));
                        switch (x[i][j]) {
                            case 1: t.classList.add("tot_den");
                                break;
                            case 5: t.classList.add("xe_den");
                                break;
                            case 2: t.classList.add("ma_den");
                                break;
                            case 3: t.classList.add("tuong_den");
                                break;
                            case 9: t.classList.add("hau_den");
                                break;
                            case 10: t.classList.add("vua_den");
                                break;
                            case 11: t.classList.add("tot_trang");
                                break;
                            case 15: t.classList.add("xe_trang");
                                break;
                            case 12: t.classList.add("ma_trang");
                                break;
                            case 13: t.classList.add("tuong_trang");
                                break;
                            case 19: t.classList.add("hau_trang");
                                break;
                            case 20: t.classList.add("vua_trang");
                                break;
                        }

                    }
                }
                second1 = x[3][9] % 100;
                minute1 = Math.floor(x[3][9] / 100);
                second2 = x[4][9] % 100;
                minute2 = Math.floor(x[4][9] / 100);
                // Lấy giá trị 'webIsOpen' từ localStorage
                updateDB = setInterval(() => {
                    fetch("/Chess/GetData")
                        .then(response => response.json())
                        .then(data => {
                            data.forEach(item => {
                                x[item.x][item.y] = item.value;
                            })
                            luot = x[1][9];
                            for (let i = 1; i <= 8; i++) {
                                for (let j = 1; j <= 8; j++) {
                                    let t = document.getElementById(String(i + ',' + j));
                                    t.classList.remove("tot_den", "tot_trang", "xe_den", "xe_trang", "ma_den", "ma_trang", "tuong_den", "tuong_trang", "hau_den", "hau_trang", "vua_den", "vua_trang");
                                    switch (x[i][j]) {
                                        case 1: t.classList.add("tot_den");
                                            break;
                                        case 5: t.classList.add("xe_den");
                                            break;
                                        case 2: t.classList.add("ma_den");
                                            break;
                                        case 3: t.classList.add("tuong_den");
                                            break;
                                        case 9: t.classList.add("hau_den");
                                            break;
                                        case 10: t.classList.add("vua_den");
                                            break;
                                        case 11: t.classList.add("tot_trang");
                                            break;
                                        case 15: t.classList.add("xe_trang");
                                            break;
                                        case 12: t.classList.add("ma_trang");
                                            break;
                                        case 13: t.classList.add("tuong_trang");
                                            break;
                                        case 19: t.classList.add("hau_trang");
                                            break;
                                        case 20: t.classList.add("vua_trang");
                                            break;
                                    }

                                }
                            }

                            
                        })
                        .catch(error => alert("that bai"));
                },100)
                timerun2 = setInterval(() => {
                    countdown();
                }, 100);
                clearInterval(Start);

            })
            .catch(error => alert("that bai"));
        
    }
}, 300);


async function UpDate(a, b, c) {
    try {
        const response = await fetch("/Chess/UpdateData", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ X: a, Y: b, Value: c })
        });
    } catch (error) {
        console.error('Lỗi trong fetch:', error);
    }
}



for (let i = 1; i <= 8; i++) {
    for (let j = 1; j <= 8; j++) {
        let t = document.getElementById(String(i + ',' + j));
        switch (x[i][j]) {
            case 1: t.classList.add("tot_den");
                break;
            case 5: t.classList.add("xe_den");
                break;
            case 2: t.classList.add("ma_den");
                break;
            case 3: t.classList.add("tuong_den");
                break;
            case 9: t.classList.add("hau_den");
                break;
            case 10: t.classList.add("vua_den");
                break;
            case 11: t.classList.add("tot_trang");
                break;
            case 15: t.classList.add("xe_trang");
                break;
            case 12: t.classList.add("ma_trang");
                break;
            case 13: t.classList.add("tuong_trang");
                break;
            case 19: t.classList.add("hau_trang");
                break;
            case 20: t.classList.add("vua_trang");
                break;
        }

    }
}


function swap() {
    e = document.getElementById("main1");
    w = document.getElementsByClassName('node');
    if (b == 1) {
        e.classList.add("swapp");
        b = 2
    } else if (b == 2) {
        e.classList.remove("swapp");
        e.classList.add("swappp");
        b = 3;
    } else {
        e.classList.remove("swappp");
        b = 1;
    }
}


var history = [];
for (let i = 1; i <= 10000; i++) {
    history[i] = new Array();
    for (let j = 1; j <= 8; j++) {
        history[i][j] = new Array();
    }
}

var h = x[2][9];
var m = 1;
var d;
var f;
var luot = x[1][9];
for (let i = 1; i <= 8; i++) {
    for (let j = 1; j <= 8; j++) {
        history[1][i][j] = x[i][j];
    }
}
function check(a, b) {
    if (luot == 1) {

        if (m == 1 && (x[a][b] == 0 || x[a][b] <= 10)) {
            return;
        }
        for (let i = 1; i <= 8; i++) {
            for (let j = 1; j <= 8; j++) {
                checked4(i, j);
                checked3(i, j);
            }
        }
        if (m == -1 && x[a][b] > 10 && x[d][f] > 10) {
            if (a == d && b == f) {
                switch (x[a][b]) {
                    case 11: tot_trang_off(a, b);
                        return;
                    case 12: ma_trang_off(a, b);
                        return;
                    case 13: tuong_trang_off(a, b);
                        return;
                    case 15: xe_trang_off(a, b);
                        return;
                    case 19: hau_trang_off(a, b);
                        return;
                    case 20: vua_trang_off(a, b);
                        return;
                }
            } else {
                switch (x[a][b]) {
                    case 11: switch (x[d][f]) {
                        case 11: tot_trang_off(d, f);
                            tot_trang_active(a, b);
                            break;
                        case 12: ma_trang_off(d, f);
                            tot_trang_active(a, b);
                            break;
                        case 13: tuong_trang_off(d, f);
                            tot_trang_active(a, b);
                            break;
                        case 15: xe_trang_off(d, f);
                            tot_trang_active(a, b);
                            break;
                        case 19: hau_trang_off(d, f);
                            tot_trang_active(a, b);
                            break;
                        case 20: vua_trang_off(d, f);
                            tot_trang_active(a, b);
                            break;
                    }
                        break;
                    case 12: switch (x[d][f]) {
                        case 11: tot_trang_off(d, f);
                            ma_trang_active(a, b);
                            break;
                        case 12: ma_trang_off(d, f);
                            ma_trang_active(a, b);
                            break;
                        case 13: tuong_trang_off(d, f);
                            ma_trang_active(a, b);
                            break;
                        case 15: xe_trang_off(d, f);
                            ma_trang_active(a, b);
                            break;
                        case 19: hau_trang_off(d, f);
                            ma_trang_active(a, b);
                            break;
                        case 20: vua_trang_off(d, f);
                            ma_trang_active(a, b);
                            break;
                    }
                        break;
                    case 13: switch (x[d][f]) {
                        case 11: tot_trang_off(d, f);
                            tuong_trang_active(a, b);
                            break;
                        case 12: ma_trang_off(d, f);
                            tuong_trang_active(a, b);
                            break;
                        case 13: tuong_trang_off(d, f);
                            tuong_trang_active(a, b);
                            break;
                        case 15: xe_trang_off(d, f);
                            tuong_trang_active(a, b);
                            break;
                        case 19: hau_trang_off(d, f);
                            tuong_trang_active(a, b);
                            break;
                        case 20: vua_trang_off(d, f);
                            tuong_trang_active(a, b);
                            break;
                    }
                        break;
                    case 15: switch (x[d][f]) {
                        case 11: tot_trang_off(d, f);
                            xe_trang_active(a, b);
                            break;
                        case 12: ma_trang_off(d, f);
                            xe_trang_active(a, b);
                            break;
                        case 13: tuong_trang_off(d, f);
                            xe_trang_active(a, b);
                            break;
                        case 15: xe_trang_off(d, f);
                            xe_trang_active(a, b);
                            break;
                        case 19: hau_trang_off(d, f);
                            xe_trang_active(a, b);
                            break;
                        case 20: if ((x[8][2] == 0 && x[8][3] == 0 && x[8][4] == 0 && b == 1 && a == 8) || (x[8][6] == 0 && x[8][7] == 0 && a == 8 && b == 8)) {
                            nhapthanh(a, b);
                        } else {
                            vua_trang_off(d, f);
                            xe_trang_active(a, b);
                        }
                            break;
                    }
                        break;
                    case 19: switch (x[d][f]) {
                        case 11: tot_trang_off(d, f);
                            hau_trang_active(a, b);
                            break;
                        case 12: ma_trang_off(d, f);
                            hau_trang_active(a, b);
                            break;
                        case 13: tuong_trang_off(d, f);
                            hau_trang_active(a, b);
                            break;
                        case 15: xe_trang_off(d, f);
                            hau_trang_active(a, b);
                            break;
                        case 19: hau_trang_off(d, f);
                            hau_trang_active(a, b);
                            break;
                        case 20: vua_trang_off(d, f);
                            hau_trang_active(a, b);
                            break;
                    }
                        break;
                    case 20: switch (x[d][f]) {
                        case 11: tot_trang_off(d, f);
                            vua_trang_active(a, b);
                            break;
                        case 12: ma_trang_off(d, f);
                            vua_trang_active(a, b);
                            break;
                        case 13: tuong_trang_off(d, f);
                            vua_trang_active(a, b);
                            break;
                        case 15: xe_trang_off(d, f);
                            vua_trang_active(a, b);
                            break;
                        case 19: hau_trang_off(d, f);
                            vua_trang_active(a, b);
                            break;
                        case 20: vua_trang_off(d, f);
                            vua_trang_active(a, b);
                            break;
                    }
                        break;
                }
            }

        }
        if (x[a][b] == 11) {
            if (m == 1) {
                tot_trang_active(a, b);
            }
        }
        else if (x[a][b] == 0 || x[a][b] <= 10) {
            if (m == -1 && x[d][f] == 11) {
                tot_trang_run(a, b);
            } else if (m == -1 && x[d][f] == 15) {
                xe_trang_run(a, b);
            } else if (m == -1 && x[d][f] == 12) {
                ma_trang_run(a, b);
            } else if (m == -1 && x[d][f] == 13) {
                tuong_trang_run(a, b);
            } else if (m == -1 && x[d][f] == 19) {
                hau_trang_run(a, b);
            } else if (m == -1 && x[d][f] == 20) {
                vua_trang_run(a, b);
                return;
            }
            let v = chieutuong_trang();
            for (let i = 1; i <= 8; i++) {
                for (let j = 1; j <= 8; j++) {
                    if (v[i][j] == 1 && x[i][j] == 20) {
                        Return();
                    }
                }
            }
        } else if (x[a][b] == 15) {
            if (m == 1) {
                xe_trang_active(a, b);
            }
        } else if (x[a][b] == 12) {
            if (m == 1) {
                ma_trang_active(a, b);
            }
        } else if (x[a][b] == 13) {
            if (m == 1) {
                tuong_trang_active(a, b);
            }
        } else if (x[a][b] == 19) {
            if (m == 1) {
                hau_trang_active(a, b);
            }
        } else if (x[a][b] == 20) {
            if (m == 1) {
                vua_trang_active(a, b);
            }
        }

        for (let i = 1; i <= 8; i++) {
            if (x[1][i] == 11) {
                let t = document.getElementById(String(1 + ',' + i));
                t.classList.remove("tot_trang");
                t.classList.add("hau_trang");
                x[1][i] = 19;
            }
        }
        let t = chieutuong_den();
        for (let i = 1; i <= 8; i++) {
            for (let j = 1; j <= 8; j++) {
                if ((t[i][j] == 1 && x[i][j] == 10)) {
                    check3(i, j);
                    if (chieuhet_den() == 0) {
                        setTimeout(e => {
                            alert("Trắng Thắng!!!");
                        }, 500);
                    }
                }
            }
        }
    } else if (luot == 0) {
        if (m == 1 && (x[a][b] == 0 || x[a][b] > 10)) {
            return;
        }
        for (let i = 1; i <= 8; i++) {
            for (let j = 1; j <= 8; j++) {
                checked4(i, j);
                checked3(i, j);
            }
        }
        if (m == -1 && x[a][b] > 0 && x[a][b] <= 10 && x[d][f] > 0 && x[d][f] <= 10) {
            if (a == d && b == f) {
                switch (x[a][b]) {
                    case 1: tot_den_off(a, b);
                        return;
                    case 2: ma_den_off(a, b);
                        return;
                    case 3: tuong_den_off(a, b);
                        return;
                    case 5: xe_den_off(a, b);
                        return;
                    case 9: hau_den_off(a, b);
                        return;
                    case 10: vua_den_off(a, b);
                        return;
                }
            } else {
                switch (x[a][b]) {
                    case 1: switch (x[d][f]) {
                        case 1: tot_den_off(d, f);
                            tot_den_active(a, b);
                            break;
                        case 2: ma_den_off(d, f);
                            tot_den_active(a, b);
                            break;
                        case 3: tuong_den_off(d, f);
                            tot_den_active(a, b);
                            break;
                        case 5: xe_den_off(d, f);
                            tot_den_active(a, b);
                            break;
                        case 9: hau_den_off(d, f);
                            tot_den_active(a, b);
                            break;
                        case 10: vua_den_off(d, f);
                            tot_den_active(a, b);
                            break;
                    }
                        break;
                    case 2: switch (x[d][f]) {
                        case 1: tot_den_off(d, f);
                            ma_den_active(a, b);
                            break;
                        case 2: ma_den_off(d, f);
                            ma_den_active(a, b);
                            break;
                        case 3: tuong_den_off(d, f);
                            ma_den_active(a, b);
                            break;
                        case 5: xe_den_off(d, f);
                            ma_den_active(a, b);
                            break;
                        case 9: hau_den_off(d, f);
                            ma_den_active(a, b);
                            break;
                        case 10: vua_den_off(d, f);
                            ma_den_active(a, b);
                            break;
                    }
                        break;
                    case 3: switch (x[d][f]) {
                        case 1: tot_den_off(d, f);
                            tuong_den_active(a, b);
                            break;
                        case 2: ma_den_off(d, f);
                            tuong_den_active(a, b);
                            break;
                        case 3: tuong_den_off(d, f);
                            tuong_den_active(a, b);
                            break;
                        case 5: xe_den_off(d, f);
                            tuong_den_active(a, b);
                            break;
                        case 9: hau_den_off(d, f);
                            tuong_den_active(a, b);
                            break;
                        case 10: vua_den_off(d, f);
                            tuong_den_active(a, b);
                            break;
                    }
                        break;
                    case 5: switch (x[d][f]) {
                        case 1: tot_den_off(d, f);
                            xe_den_active(a, b);
                            break;
                        case 2: ma_den_off(d, f);
                            xe_den_active(a, b);
                            break;
                        case 3: tuong_den_off(d, f);
                            xe_den_active(a, b);
                            break;
                        case 5: xe_den_off(d, f);
                            xe_den_active(a, b);
                            break;
                        case 9: hau_den_off(d, f);
                            xe_den_active(a, b);
                            break;
                        case 10:
                            if ((x[1][2] == 0 && x[1][3] == 0 && x[1][4] == 0 && b == 1 && a == 1) || (x[1][6] == 0 && x[1][7] == 0 && a == 1 && b == 8)) {
                                nhapthanh(a, b);
                            } else {
                                vua_den_off(d, f);
                                xe_den_active(a, b);
                            }
                            break;
                    }
                        break;
                    case 9: switch (x[d][f]) {
                        case 1: tot_den_off(d, f);
                            hau_den_active(a, b);
                            break;
                        case 2: ma_den_off(d, f);
                            hau_den_active(a, b);
                            break;
                        case 3: tuong_den_off(d, f);
                            hau_den_active(a, b);
                            break;
                        case 5: xe_den_off(d, f);
                            hau_den_active(a, b);
                            break;
                        case 9: hau_den_off(d, f);
                            hau_den_active(a, b);
                            break;
                        case 10: vua_den_off(d, f);
                            hau_den_active(a, b);
                            break;
                    }
                        break;
                    case 10: switch (x[d][f]) {
                        case 1: tot_den_off(d, f);
                            vua_den_active(a, b);
                            break;
                        case 2: ma_den_off(d, f);
                            vua_den_active(a, b);
                            break;
                        case 3: tuong_den_off(d, f);
                            vua_den_active(a, b);
                            break;
                        case 5: xe_den_off(d, f);
                            vua_den_active(a, b);
                            break;
                        case 9: hau_den_off(d, f);
                            vua_den_active(a, b);
                            break;
                        case 10: vua_den_off(d, f);
                            vua_den_active(a, b);
                            break;
                    }
                        break;
                }
            }

        }
        if (x[a][b] == 1) {
            if (m == 1) {
                tot_den_active(a, b);
            }
        }
        else if (x[a][b] == 0 || x[a][b] > 10) {
            if (m == -1 && x[d][f] == 1) {
                tot_den_run(a, b);
            } else if (m == -1 && x[d][f] == 5) {
                xe_den_run(a, b);
            } else if (m == -1 && x[d][f] == 2) {
                ma_den_run(a, b);
            } else if (m == -1 && x[d][f] == 3) {
                tuong_den_run(a, b);
            } else if (m == -1 && x[d][f] == 9) {
                hau_den_run(a, b);
            } else if (m == -1 && x[d][f] == 10) {
                vua_den_run(a, b);
                return;
            }
            let v = chieutuong_den();
            for (let i = 1; i <= 8; i++) {
                for (let j = 1; j <= 8; j++) {
                    if (v[i][j] == 1 && x[i][j] == 10) {
                        Return();
                    }
                }
            }
        } else if (x[a][b] == 5) {
            if (m == 1) {
                xe_den_active(a, b);
            }
        } else if (x[a][b] == 2) {
            if (m == 1) {
                ma_den_active(a, b);
            }
        } else if (x[a][b] == 3) {
            if (m == 1) {
                tuong_den_active(a, b);
            }
        } else if (x[a][b] == 9) {
            if (m == 1) {
                hau_den_active(a, b);
            }
        } else if (x[a][b] == 10) {
            if (m == 1) {
                vua_den_active(a, b);
            }
        }

        for (let i = 1; i <= 8; i++) {
            if (x[8][i] == 1) {
                let t = document.getElementById(String(8 + ',' + i));
                t.classList.remove("tot_den");
                t.classList.add("hau_den");
                x[8][i] = 9;
            }
        }
        let v = chieutuong_trang();
        for (let i = 1; i <= 8; i++) {
            for (let j = 1; j <= 8; j++) {
                if (v[i][j] == 1 && x[i][j] == 20) {
                    check3(i, j);
                    if (chieuhet_trang() == 0) {
                        setTimeout(e => {
                            alert("Đen Thắng!!!");
                        }, 500);
                    }
                }
            }
        }
    }

}
function check2(b, c) {
    let y = document.getElementById(String(b + ',' + c)).querySelector('.dot');
    y.classList.add("Active");
}
function check3(b, c) {
    let y = document.getElementById(String(b + ',' + c)).querySelector('.reddot');
    y.classList.add("Active");
}
function check4(b, c) {
    let y = document.getElementById(String(b + ',' + c));
    y.classList.add("xanh");
}
function checked2(b, c) {
    let y = document.getElementById(String(b + ',' + c)).querySelector('.dot');
    y.classList.remove("Active");
}
function checked3(b, c) {
    let y = document.getElementById(String(b + ',' + c)).querySelector('.reddot');
    y.classList.remove("Active");
}
function checked4(b, c) {
    let y = document.getElementById(String(b + ',' + c));
    y.classList.remove("xanh");
}
function anquan_den(a, b, d, f, s) {
    switch (x[a][b]) {
        case 0: let t0 = document.getElementById(String(a + ',' + b));
            let y0 = document.getElementById(String(d + ',' + f));
            t0.classList.add(s);
            y0.classList.remove(s);
            break;
        case 1: let t11 = document.getElementById(String(a + ',' + b));
            let y11 = document.getElementById(String(d + ',' + f));
            t11.classList.remove("tot_den");
            t11.classList.add(s);
            y11.classList.remove(s);
            break;
        case 5: let t15 = document.getElementById(String(a + ',' + b));
            let y15 = document.getElementById(String(d + ',' + f));
            t15.classList.remove("xe_den");
            t15.classList.add(s);
            y15.classList.remove(s);
            break;
        case 2: let t12 = document.getElementById(String(a + ',' + b));
            let y12 = document.getElementById(String(d + ',' + f));
            t12.classList.remove("ma_den");
            t12.classList.add(s);
            y12.classList.remove(s);
            break;
        case 3: let t13 = document.getElementById(String(a + ',' + b));
            let y13 = document.getElementById(String(d + ',' + f));
            t13.classList.remove("tuong_den");
            t13.classList.add(s);
            y13.classList.remove(s);
            break;
        case 9: let t19 = document.getElementById(String(a + ',' + b));
            let y19 = document.getElementById(String(d + ',' + f));
            t19.classList.remove("hau_den");
            t19.classList.add(s);
            y19.classList.remove(s);
            break;
        case 10: let t20 = document.getElementById(String(a + ',' + b));
            let y20 = document.getElementById(String(d + ',' + f));
            t20.classList.remove("vua_den");
            t20.classList.add(s);
            y20.classList.remove(s);
            break;
    }
    UpDate(a, b, x[d][f]);
    UpDate(d, f, 0);
    x[a][b] = x[d][f];
    x[d][f] = 0;
    check4(d, f);
    check4(a, b);
    h++;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            history[h][i][j] = x[i][j];
        }
    }
    
}
function anquan_trang(a, b, d, f, s) {
    switch (x[a][b]) {
        case 0: let t0 = document.getElementById(String(a + ',' + b));
            let y0 = document.getElementById(String(d + ',' + f));
            t0.classList.add(s);
            y0.classList.remove(s);
            break;
        case 11: let t11 = document.getElementById(String(a + ',' + b));
            let y11 = document.getElementById(String(d + ',' + f));
            t11.classList.remove("tot_trang");
            t11.classList.add(s);
            y11.classList.remove(s);
            break;
        case 15: let t15 = document.getElementById(String(a + ',' + b));
            let y15 = document.getElementById(String(d + ',' + f));
            t15.classList.remove("xe_trang");
            t15.classList.add(s);
            y15.classList.remove(s);
            break;
        case 12: let t12 = document.getElementById(String(a + ',' + b));
            let y12 = document.getElementById(String(d + ',' + f));
            t12.classList.remove("ma_trang");
            t12.classList.add(s);
            y12.classList.remove(s);
            break;
        case 13: let t13 = document.getElementById(String(a + ',' + b));
            let y13 = document.getElementById(String(d + ',' + f));
            t13.classList.remove("tuong_trang");
            t13.classList.add(s);
            y13.classList.remove(s);
            break;
        case 19: let t19 = document.getElementById(String(a + ',' + b));
            let y19 = document.getElementById(String(d + ',' + f));
            t19.classList.remove("hau_trang");
            t19.classList.add(s);
            y19.classList.remove(s);
            break;
    }
    UpDate(a, b, x[d][f]);
    UpDate(d, f, 0);
    x[a][b] = x[d][f];
    x[d][f] = 0;
    check4(d, f);
    check4(a, b);
    h++;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            history[h][i][j] = x[i][j];
        }
    }
}
function tot_den_active(a, b) {
    check2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if (a == 2 && ((i == a + 1 && j == b) || (i == a + 2 && j == b))) {
                    check2(i, j);
                } else if (a != 2 && i == a + 1 && j == b) {
                    check2(i, j);
                }
            } else if (x[i][j] > 10) {
                if ((i == a + 1 && j == b - 1) || (i == a + 1 && j == b + 1)) {
                    check3(i, j);
                }
            }
        }
    }
    m = -1;
    d = a;
    f = b;
}
function tot_den_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if (a == 2 && ((i == a + 1 && j == b) || (i == a + 2 && j == b))) {
                    checked2(i, j);
                } else if (a != 2 && i == a + 1 && j == b) {
                    checked2(i, j);
                }
            } else if (x[i][j] > 10) {
                if ((i == a + 1 && j == b - 1) || (i == a + 1 && j == b + 1)) {
                    checked3(i, j);
                }
            }
        }
    }
    m = -m;
}
function tot_den_run(a, b) {
    let tot = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if (d == 2 && ((i == d + 1 && j == f) || (i == d + 2 && j == f))) {
                    tot[p] = String(i + '.' + j);
                    p++;
                } else if (d != 2 && i == d + 1 && j == f) {
                    tot[p] = String(i + '.' + j);
                    p++;
                }
            } else if (x[i][j] > 10) {
                if ((i == d + 1 && j == f - 1) || (i == d + 1 && j == f + 1)) {
                    tot[p] = String(i + '.' + j);
                    p++;
                }
            }
        }
    }
    for (let i = 1; i <= tot.length; i++) {
        if (String(a + '.' + b) == tot[i]) {
            tot_den_off(d, f);
            anquan_trang(a, b, d, f, "tot_den")
            d = 0;
            f = 0;
            luot = 1; UpDate(1, 9, luot);
            o = 1;
            break;
        }
    }
    if (o == 0) {
        tot_den_off(d, f);
    }
}
function xe_den_active(a, b) {
    check2(a, b);
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            check3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            check3(a, i);
        }
        break;
    }
    m = -m;
    d = a;
    f = b;
}
function xe_den_off(a, b) {
    checked2(a, b);
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            checked3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            checked3(a, i);
        }
        break;
    }
    m = -m;
}
function xe_den_run(a, b) {
    let xe = [];
    let p = 1;
    let o = 0;
    for (let i = d - 1; i >= 1; i--) {
        if (x[i][f] == 0) {
            xe[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] > 10) {
            xe[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = d + 1; i <= 8; i++) {
        if (x[i][f] == 0) {
            xe[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] > 10) {
            xe[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = f - 1; i >= 1; i--) {
        if (x[d][i] == 0) {
            xe[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] > 10) {
            xe[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    for (let i = f + 1; i <= 8; i++) {
        if (x[d][i] == 0) {
            xe[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] > 10) {
            xe[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    for (let i = 1; i <= xe.length; i++) {
        if (String(a + '.' + b) == xe[i]) {
            xe_den_off(d, f);
            anquan_trang(a, b, d, f, "xe_den")
            d = 0; f = 0;
            o = 1;
            luot = 1; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        xe_den_off(d, f);
    }

}
function ma_den_active(a, b) {
    check2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    check2(i, j);
                }
            } else if (x[i][j] > 10) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    check3(i, j);
                }
            }
        }
    }
    m = -1;
    d = a;
    f = b;
}
function ma_den_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    checked2(i, j);
                }
            } else if (x[i][j] > 10) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    checked3(i, j);
                }
            }
        }
    }
    m = -m;
}
function ma_den_run(a, b) {
    let ma = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0 || x[i][j] > 10) {
                if ((i == d - 1 && j == f - 2) || (i == d + 1 && j == f - 2) || (i == d + 2 && j == f - 1) || (i == d + 2 && j == f + 1) || (i == d + 1 && j == f + 2) || (i == d - 1 && j == f + 2) || (i == d - 2 && j == f + 1) || (i == d - 2 && j == f - 1)) {
                    ma[p] = String(i + '.' + j);
                    p++;
                }
            }
        }
    }
    for (let i = 1; i <= ma.length; i++) {
        if (String(a + '.' + b) == ma[i]) {
            ma_den_off(d, f);
            anquan_trang(a, b, d, f, "ma_den")
            d = 0; f = 0;
            o = 1;
            luot = 1; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        ma_den_off(d, f);
    }
}
function tuong_den_active(a, b) {
    check2(a, b);
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                check2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] > 10) {
                check3(a - i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                check2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] > 10) {
                check3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                check2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] > 10) {
                check3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                check2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] > 10) {
                check3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
    d = a;
    f = b;

}
function tuong_den_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                checked2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] > 10) {
                checked3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                checked2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] > 10) {
                checked3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                checked2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] > 10) {
                checked3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                checked2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] > 10) {
                checked3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;

}
function tuong_den_run(a, b) {
    let tuong = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f - i > 0) {
            if (x[d - i][f - i] == 0) {
                tuong[p] = String((d - i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d - i][f - i] > 10) {
                tuong[p] = String((d - i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f + i <= 8) {
            if (x[d + i][f + i] == 0) {
                tuong[p] = String((d + i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d + i][f + i] > 10) {
                tuong[p] = String((d + i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f + i <= 8) {
            if (x[d - i][f + i] == 0) {
                tuong[p] = String((d - i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d - i][f + i] > 10) {
                tuong[p] = String((d - i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f - i > 0) {
            if (x[d + i][f - i] == 0) {
                tuong[p] = String((d + i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d + i][f - i] > 10) {
                tuong[p] = String((d + i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= tuong.length; i++) {
        if (String(a + '.' + b) == tuong[i]) {
            tuong_den_off(d, f);
            anquan_trang(a, b, d, f, "tuong_den")
            d = 0; f = 0;
            o = 1;
            luot = 1; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        tuong_den_off(d, f);
    }

}

function hau_den_active(a, b) {
    check2(a, b);
    // xe
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            check3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            check3(a, i);
        }
        break;
    }
    // tuong
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                check2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] > 10) {
                check3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                check2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] > 10) {
                check3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                check2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] > 10) {
                check3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                check2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] > 10) {
                check3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
    d = a;
    f = b;
}
function hau_den_off(a, b) {
    checked2(a, b);
    // xe
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] > 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            checked3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] > 10) {
            checked3(a, i);
        }
        break;
    }
    // tuong
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                checked2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] > 10) {
                checked3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                checked2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] > 10) {
                checked3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                checked2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] > 10) {
                checked3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                checked2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] > 10) {
                checked3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
}
function hau_den_run(a, b) {
    let hau = [];
    let p = 1;
    let o = 0;
    // xe
    for (let i = d - 1; i >= 1; i--) {
        if (x[i][f] == 0) {
            hau[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] > 10) {
            hau[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = d + 1; i <= 8; i++) {
        if (x[i][f] == 0) {
            hau[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] > 10) {
            hau[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = f - 1; i >= 1; i--) {
        if (x[d][i] == 0) {
            hau[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] > 10) {
            hau[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    for (let i = f + 1; i <= 8; i++) {
        if (x[d][i] == 0) {
            hau[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] > 10) {
            hau[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    // tuong
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f - i > 0) {
            if (x[d - i][f - i] == 0) {
                hau[p] = String((d - i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d - i][f - i] > 10) {
                hau[p] = String((d - i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f + i <= 8) {
            if (x[d + i][f + i] == 0) {
                hau[p] = String((d + i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d + i][f + i] > 10) {
                hau[p] = String((d + i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f + i <= 8) {
            if (x[d - i][f + i] == 0) {
                hau[p] = String((d - i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d - i][f + i] > 10) {
                hau[p] = String((d - i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f - i > 0) {
            if (x[d + i][f - i] == 0) {
                hau[p] = String((d + i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d + i][f - i] > 10) {
                hau[p] = String((d + i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break;
        }
    }

    for (let i = 1; i <= hau.length; i++) {
        if (String(a + '.' + b) == hau[i]) {
            hau_den_off(d, f);
            anquan_trang(a, b, d, f, "hau_den")
            d = 0; f = 0;
            o = 1;
            luot = 1; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        hau_den_off(d, f);
    }
}
function vua_den_active(a, b) {
    check2(a, b);
    let u = chieutuong_den();
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0 && u[i][j] != 1) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    check2(i, j);
                }
            } else if (x[i][j] > 10) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    check3(i, j);
                }
            }
        }
    }
    if (a == 1 && b == 5 && x[1][1] == 5 && x[1][2] == 0 && x[1][3] == 0 && x[1][4] == 0) {
        check3(1, 1);
    }
    if (a == 1 && b == 5 && x[1][8] == 5 && x[1][6] == 0 && x[1][7] == 0) {
        check3(1, 8);
    }
    m = -1;
    d = a;
    f = b;
}
function vua_den_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    checked2(i, j);
                }
            } else if (x[i][j] > 10) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    checked3(i, j);
                }
            }
        }
    }
    if (a == 1 && b == 5 && x[1][1] == 5 && x[1][2] == 0 && x[1][3] == 0 && x[1][4] == 0) {
        checked3(1, 1);
    }
    if (a == 1 && b == 5 && x[1][8] == 5 && x[1][6] == 0 && x[1][7] == 0) {
        checked3(1, 8);
    }
    m = -m;
}
function vua_den_run(a, b) {
    let u = chieutuong_den();
    let vua = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if ((x[i][j] == 0 || x[i][j] > 10) && u[i][j] != 1) {
                if ((i == d - 1 && j == f - 1) || (i == d && j == f - 1) || (i == d + 1 && j == f - 1) || (i == d + 1 && j == f) || (i == d + 1 && j == f + 1) || (i == d && j == f + 1) || (i == d - 1 && j == f + 1) || (i == d - 1 && j == f)) {
                    vua[p] = String(i + '.' + j);
                    p++;
                }
            }
        }
    }
    for (let i = 1; i <= vua.length; i++) {
        if (String(a + '.' + b) == vua[i]) {
            vua_den_off(d, f);
            anquan_trang(a, b, d, f, "vua_den")
            d = 0; f = 0;
            o = 1;
            luot = 1; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        vua_den_off(d, f);
    }
}
// TRANG


function tot_trang_active(a, b) {
    check2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if (a == 7 && ((i == a - 1 && j == b) || (i == a - 2 && j == b))) {
                    check2(i, j);
                } else if (a != 7 && i == a - 1 && j == b) {
                    check2(i, j);
                }
            } else if (x[i][j] <= 10) {
                if ((i == a - 1 && j == b - 1) || (i == a - 1 && j == b + 1)) {
                    check3(i, j);
                }
            }
        }
    }
    m = -1;
    d = a;
    f = b;
}
function tot_trang_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if (a == 7 && ((i == a - 1 && j == b) || (i == a - 2 && j == b))) {
                    checked2(i, j);
                } else if (a != 7 && i == a - 1 && j == b) {
                    checked2(i, j);
                }
            } else if (x[i][j] <= 10) {
                if ((i == a - 1 && j == b - 1) || (i == a - 1 && j == b + 1)) {
                    checked3(i, j);
                }
            }
        }
    }
    m = -m;
}
function tot_trang_run(a, b) {
    let tot = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if (d == 7 && ((i == d - 1 && j == f) || (i == d - 2 && j == f))) {
                    tot[p] = String(i + '.' + j);
                    p++;
                } else if (d != 7 && i == d - 1 && j == f) {
                    tot[p] = String(i + '.' + j);
                    p++;
                }
            } else if (x[i][j] <= 10) {
                if ((i == d - 1 && j == f - 1) || (i == d - 1 && j == f + 1)) {
                    tot[p] = String(i + '.' + j);
                    p++;
                }
            }
        }
    }
    for (let i = 1; i <= tot.length; i++) {
        if (String(a + '.' + b) == tot[i]) {
            tot_trang_off(d, f);
            anquan_den(a, b, d, f, "tot_trang");
            d = 0; f = 0;
            o = 1;
            luot = 0; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        tot_trang_off(d, f);
    }
}
function xe_trang_active(a, b) {
    check2(a, b);
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            check3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            check3(a, i);
        }
        break;
    }
    m = -m;
    d = a;
    f = b;
}
function xe_trang_off(a, b) {
    checked2(a, b);
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            checked3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            checked3(a, i);
        }
        break;
    }
    m = -m;
}
function xe_trang_run(a, b) {
    let xe = [];
    let p = 1;
    let o = 0;
    for (let i = d - 1; i >= 1; i--) {
        if (x[i][f] == 0) {
            xe[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] <= 10) {
            xe[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = d + 1; i <= 8; i++) {
        if (x[i][f] == 0) {
            xe[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] <= 10) {
            xe[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = f - 1; i >= 1; i--) {
        if (x[d][i] == 0) {
            xe[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] <= 10) {
            xe[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    for (let i = f + 1; i <= 8; i++) {
        if (x[d][i] == 0) {
            xe[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] <= 10) {
            xe[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    for (let i = 1; i <= xe.length; i++) {
        if (String(a + '.' + b) == xe[i]) {
            xe_trang_off(d, f);
            anquan_den(a, b, d, f, "xe_trang");
            d = 0; f = 0;
            o = 1;
            luot = 0; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        xe_trang_off(d, f);
    }
}
function ma_trang_active(a, b) {
    check2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    check2(i, j);
                }
            } else if (x[i][j] <= 10) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    check3(i, j);
                }
            }
        }
    }
    m = -1;
    d = a;
    f = b;
}
function ma_trang_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    checked2(i, j);
                }
            } else if (x[i][j] <= 10) {
                if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                    checked3(i, j);
                }
            }
        }
    }
    m = -m;
}
function ma_trang_run(a, b) {
    let ma = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0 || x[i][j] <= 10) {
                if ((i == d - 1 && j == f - 2) || (i == d + 1 && j == f - 2) || (i == d + 2 && j == f - 1) || (i == d + 2 && j == f + 1) || (i == d + 1 && j == f + 2) || (i == d - 1 && j == f + 2) || (i == d - 2 && j == f + 1) || (i == d - 2 && j == f - 1)) {
                    ma[p] = String(i + '.' + j);
                    p++;
                }
            }
        }
    }
    for (let i = 1; i <= ma.length; i++) {
        if (String(a + '.' + b) == ma[i]) {
            ma_trang_off(d, f);
            anquan_den(a, b, d, f, "ma_trang");
            d = 0; f = 0;
            o = 1;
            luot = 0; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        ma_trang_off(d, f);
    }
}
function tuong_trang_active(a, b) {
    check2(a, b);
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                check2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] <= 10) {
                check3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                check2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] <= 10) {
                check3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                check2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] <= 10) {
                check3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                check2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] <= 10) {
                check3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
    d = a;
    f = b;

}
function tuong_trang_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                checked2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] <= 10) {
                checked3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                checked2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] <= 10) {
                checked3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                checked2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] <= 10) {
                checked3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                checked2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] <= 10) {
                checked3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
}
function tuong_trang_run(a, b) {
    let tuong = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f - i > 0) {
            if (x[d - i][f - i] == 0) {
                tuong[p] = String((d - i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d - i][f - i] <= 10) {
                tuong[p] = String((d - i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f + i <= 8) {
            if (x[d + i][f + i] == 0) {
                tuong[p] = String((d + i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d + i][f + i] <= 10) {
                tuong[p] = String((d + i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f + i <= 8) {
            if (x[d - i][f + i] == 0) {
                tuong[p] = String((d - i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d - i][f + i] <= 10) {
                tuong[p] = String((d - i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f - i > 0) {
            if (x[d + i][f - i] == 0) {
                tuong[p] = String((d + i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d + i][f - i] <= 10) {
                tuong[p] = String((d + i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= tuong.length; i++) {
        if (String(a + '.' + b) == tuong[i]) {
            tuong_trang_off(d, f);
            anquan_den(a, b, d, f, "tuong_trang");
            d = 0; f = 0;
            o = 1;
            luot = 0; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        tuong_trang_off(d, f);
    }
}
function hau_trang_active(a, b) {
    check2(a, b);
    // xe
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            check2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            check3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            check3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            check2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            check3(a, i);
        }
        break;
    }
    // tuong
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                check2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] <= 10) {
                check3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                check2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] <= 10) {
                check3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                check2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] <= 10) {
                check3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                check2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] <= 10) {
                check3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
    d = a;
    f = b;
}
function hau_trang_off(a, b) {
    checked2(a, b);
    // xe
    for (let i = a - 1; i >= 1; i--) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = a + 1; i <= 8; i++) {
        if (x[i][b] == 0) {
            checked2(i, b);
            continue;
        } else if (x[i][b] <= 10) {
            checked3(i, b);
        }
        break;
    }
    for (let i = b - 1; i >= 1; i--) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            checked3(a, i);
        }
        break;
    }
    for (let i = b + 1; i <= 8; i++) {
        if (x[a][i] == 0) {
            checked2(a, i);
            continue;
        } else if (x[a][i] <= 10) {
            checked3(a, i);
        }
        break;
    }
    // tuong
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b - i > 0) {
            if (x[a - i][b - i] == 0) {
                checked2(a - i, b - i);
                continue;
            } else if (x[a - i][b - i] <= 10) {
                checked3(a - i, b - i);
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b + i <= 8) {
            if (x[a + i][b + i] == 0) {
                checked2(a + i, b + i);
                continue;
            } else if (x[a + i][b + i] <= 10) {
                checked3(a + i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a - i > 0 && b + i <= 8) {
            if (x[a - i][b + i] == 0) {
                checked2(a - i, b + i);
                continue;
            } else if (x[a - i][b + i] <= 10) {
                checked3(a - i, b + i);
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (a + i <= 8 && b - i > 0) {
            if (x[a + i][b - i] == 0) {
                checked2(a + i, b - i);
                continue;
            } else if (x[a + i][b - i] <= 10) {
                checked3(a + i, b - i);
            }
            break;
        } else {
            break;
        }
    }
    m = -m;
}
function hau_trang_run(a, b) {
    let hau = [];
    let p = 1;
    let o = 0;
    // xe
    for (let i = d - 1; i >= 1; i--) {
        if (x[i][f] == 0) {
            hau[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] <= 10) {
            hau[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = d + 1; i <= 8; i++) {
        if (x[i][f] == 0) {
            hau[p] = String(i + '.' + f);
            p++;
            continue;
        } else if (x[i][f] <= 10) {
            hau[p] = String(i + '.' + f);
            p++;
        }
        break;
    }
    for (let i = f - 1; i >= 1; i--) {
        if (x[d][i] == 0) {
            hau[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] <= 10) {
            hau[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    for (let i = f + 1; i <= 8; i++) {
        if (x[d][i] == 0) {
            hau[p] = String(d + '.' + i);
            p++;
            continue;
        } else if (x[d][i] <= 10) {
            hau[p] = String(d + '.' + i);
            p++;
        }
        break;
    }
    // tuong
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f - i > 0) {
            if (x[d - i][f - i] == 0) {
                hau[p] = String((d - i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d - i][f - i] <= 10) {
                hau[p] = String((d - i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f + i <= 8) {
            if (x[d + i][f + i] == 0) {
                hau[p] = String((d + i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d + i][f + i] <= 10) {
                hau[p] = String((d + i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d - i > 0 && f + i <= 8) {
            if (x[d - i][f + i] == 0) {
                hau[p] = String((d - i) + '.' + (f + i));
                p++;
                continue;
            } else if (x[d - i][f + i] <= 10) {
                hau[p] = String((d - i) + '.' + (f + i));
                p++;
            }
            break;
        } else {
            break;
        }
    }
    for (let i = 1; i <= 8; i++) {
        if (d + i <= 8 && f - i > 0) {
            if (x[d + i][f - i] == 0) {
                hau[p] = String((d + i) + '.' + (f - i));
                p++;
                continue;
            } else if (x[d + i][f - i] <= 10) {
                hau[p] = String((d + i) + '.' + (f - i));
                p++;
            }
            break;
        } else {
            break;
        }
    }

    for (let i = 1; i <= hau.length; i++) {
        if (String(a + '.' + b) == hau[i]) {
            hau_trang_off(d, f);
            anquan_den(a, b, d, f, "hau_trang");
            d = 0; f = 0;
            o = 1;
            luot = 0; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        hau_trang_off(d, f);
    }
}
function vua_trang_active(a, b) {
    check2(a, b);
    let u = chieutuong_trang();
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0 && u[i][j] != 1) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    check2(i, j);
                }
            } else if (x[i][j] <= 10 && x[i][j] > 0) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    check3(i, j);
                }
            }
        }
    }
    if (a == 8 && b == 5 && x[8][1] == 15 && x[8][2] == 0 && x[8][3] == 0 && x[8][4] == 0) {
        check3(8, 1);
    }
    if (a == 8 && b == 5 && x[8][8] == 15 && x[8][6] == 0 && x[8][7] == 0) {
        check3(8, 8);
    }

    m = -1;
    d = a;
    f = b;
}
function vua_trang_off(a, b) {
    checked2(a, b);
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 0) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    checked2(i, j);
                }
            } else if (x[i][j] <= 10) {
                if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {
                    checked3(i, j);
                }
            }
        }
    }
    if (a == 8 && b == 5 && x[8][1] == 15 && x[8][2] == 0 && x[8][3] == 0 && x[8][4] == 0) {
        checked3(8, 1);
    }
    if (a == 8 && b == 5 && x[8][8] == 15 && x[8][6] == 0 && x[8][7] == 0) {
        checked3(8, 8);
    }
    m = -m;
}
function vua_trang_run(a, b) {
    let u = chieutuong_trang();
    let vua = [];
    let p = 1;
    let o = 0;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if ((x[i][j] == 0 || x[i][j] <= 10) && u[i][j] != 1) {
                if ((i == d - 1 && j == f - 1) || (i == d && j == f - 1) || (i == d + 1 && j == f - 1) || (i == d + 1 && j == f) || (i == d + 1 && j == f + 1) || (i == d && j == f + 1) || (i == d - 1 && j == f + 1) || (i == d - 1 && j == f)) {
                    vua[p] = String(i + '.' + j);
                    p++;
                }
            }
        }
    }
    for (let i = 1; i <= vua.length; i++) {
        if (String(a + '.' + b) == vua[i]) {
            vua_trang_off(d, f);
            anquan_den(a, b, d, f, "vua_trang");
            d = 0; f = 0;
            o = 1;
            luot = 0; UpDate(1, 9, luot);
            break;
        }
    }
    if (o == 0) {
        vua_trang_off(d, f);
    }
}
function nhapthanh(a, b) {
    if (d == 1 && f == 5 && a == 1 && b == 1) {
        let t15 = document.getElementById("1,1");
        let y15 = document.getElementById("1,5");
        let w15 = document.getElementById("1,3");
        let q15 = document.getElementById("1,4");
        t15.classList.remove("xe_den");
        y15.classList.remove("vua_den");
        w15.classList.add("vua_den");
        q15.classList.add("xe_den");
        vua_den_off(d, f);
        x[1][3] = 10;
        x[1][4] = 5;
        x[1][1] = 0;
        x[1][5] = 0;
        luot = 1; UpDate(1, 9, luot);
    } else if (d == 1 && f == 5 && a == 1 && b == 8) {
        let t15 = document.getElementById("1,8");
        let y15 = document.getElementById("1,5");
        let w15 = document.getElementById("1,7");
        let q15 = document.getElementById("1,6");
        t15.classList.remove("xe_den");
        y15.classList.remove("vua_den");
        w15.classList.add("vua_den");
        q15.classList.add("xe_den");
        vua_den_off(d, f);
        x[1][7] = 10;
        x[1][6] = 5;
        x[1][5] = 0;
        x[1][8] = 0;
        luot = 1; UpDate(1, 9, luot);
    }
    else if (d == 8 && f == 5 && a == 8 && b == 8) {
        let t15 = document.getElementById("8,8");
        let y15 = document.getElementById("8,5");
        let w15 = document.getElementById("8,7");
        let q15 = document.getElementById("8,6");
        t15.classList.remove("xe_trang");
        y15.classList.remove("vua_trang");
        w15.classList.add("vua_trang");
        q15.classList.add("xe_trang");
        vua_trang_off(d, f);
        x[8][7] = 20;
        x[8][6] = 15;
        x[8][5] = 0;
        x[8][8] = 0;
        luot = 0; UpDate(1, 9, luot);
    } else if (d == 8 && f == 5 && a == 8 && b == 1) {
        let t15 = document.getElementById("8,1");
        let y15 = document.getElementById("8,5");
        let w15 = document.getElementById("8,3");
        let q15 = document.getElementById("8,4");
        t15.classList.remove("xe_trang");
        y15.classList.remove("vua_trang");
        w15.classList.add("vua_trang");
        q15.classList.add("xe_trang");
        vua_trang_off(d, f);
        x[8][3] = 20;
        x[8][4] = 15;
        x[8][1] = 0;
        x[8][5] = 0;
        luot = 0; UpDate(1, 9, luot);
    }
    d = 0;
    f = 0;
    h++;
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            history[h][i][j] = x[i][j];
        }
    }

}
function chieutuong_trang() {
    var u = [];
    for (let i = 1; i <= 8; i++) {
        u[i] = new Array();
    }
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 1) {
                u[i + 1][j - 1] = 1;
                u[i + 1][j + 1] = 1;
            } else if (x[i][j] == 5) {
                for (let k = i - 1; k >= 1; k--) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else if (x[k][j] < 10) {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = i + 1; k <= 8; k++) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else if (x[k][j] < 10) {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = j - 1; k >= 1; k--) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else if (x[i][k] < 10) {
                        u[i][k] = 1;
                    }
                    break;
                }
                for (let k = j + 1; k <= 8; k++) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else if (x[i][k] < 10) {
                        u[i][k] = 1;
                    }
                    break;
                }
            } else if (x[i][j] == 2) {
                for (let k = 1; k <= 8; k++) {
                    for (let l = 1; l <= 8; l++) {
                        if (x[k][l] == 0 || x[k][l] == 20 || x[k][l] < 10) {
                            if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1)) {
                                u[k][l] = 1;
                            }
                        }
                    }
                }
            } else if (x[i][j] == 3) {
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j - k > 0) {
                        if (x[i - k][j - k] == 0 || x[i - k][j - k] == 20) {
                            u[i - k][j - k] = 1;
                            continue;
                        } else if (x[i - k][j - k] < 10) {
                            u[i - k][j - k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j + k <= 8) {
                        if (x[i + k][j + k] == 0 || x[i + k][j + k] == 20) {
                            u[i + k][j + k] = 1;
                            continue;
                        } else if (x[i + k][j + k] < 10) {
                            u[i + k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j + k <= 8) {
                        if (x[i - k][j + k] == 0 || x[i - k][j + k] == 20) {
                            u[i - k][j + k] = 1;
                            continue;
                        } else if (x[i - k][j + k] < 10) {
                            u[i - k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j - k > 0) {
                        if (x[i + k][j - k] == 0 || x[i + k][j - k] == 20) {
                            u[i + k][j - k] = 1;
                            continue;
                        } else if (x[i + k][j - k] < 10) {
                            u[i + k][j - k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            } else if (x[i][j] == 9) {
                for (let k = i - 1; k >= 1; k--) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else if (x[k][j] < 10) {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = i + 1; k <= 8; k++) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else if (x[k][j] < 10) {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = j - 1; k >= 1; k--) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else if (x[i][k] < 10) {
                        u[i][k] = 1;
                    }
                    break;
                }
                for (let k = j + 1; k <= 8; k++) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else if (x[i][k] < 10) {
                        u[i][k] = 1;
                    }
                    break;
                }
                // tuong
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j - k > 0) {
                        if (x[i - k][j - k] == 0 || x[i - k][j - k] == 20) {
                            u[i - k][j - k] = 1;
                            continue;
                        } else if (x[i - k][j - k] < 10) {
                            u[i - k][j - k] = 1;
                        }
                        break;
                    } else {
                        break
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j + k <= 8) {
                        if (x[i + k][j + k] == 0 || x[i + k][j + k] == 20) {
                            u[i + k][j + k] = 1;
                            continue;
                        } else if (x[i + k][j + k] < 10) {
                            u[i + k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j + k <= 8) {
                        if (x[i - k][j + k] == 0 || x[i - k][j + k] == 20) {
                            u[i - k][j + k] = 1;
                            continue;
                        } else if (x[i - k][j + k] < 10) {
                            u[i - k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j - k > 0) {
                        if (x[i + k][j - k] == 0 || x[i + k][j - k] == 20) {
                            u[i + k][j - k] = 1;
                            continue;
                        } else if (x[i + k][j - k] < 10) {
                            u[i + k][j - k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            } else if (x[i][j] == 10) {
                for (let k = 1; k <= 8; k++) {
                    for (let l = 1; l <= 8; l++) {
                        if (x[k][l] == 0 || x[k][l] == 20 || x[k][l] < 10) {
                            if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j)) {
                                u[k][l] = 1;
                            }
                        }
                    }
                }
            }
        }
    }
    return u;
}
function chieutuong_den() {
    var u = [];
    for (let i = 1; i <= 8; i++) {
        u[i] = new Array();
    }
    for (let a = 1; a <= 8; a++) {
        for (let b = 1; b <= 8; b++) {
            if (x[a][b] == 11) {
                u[a - 1][b - 1] = 1;
                u[a - 1][b + 1] = 1;
            } else if (x[a][b] == 15) {
                for (let i = a - 1; i >= 1; i--) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else if (x[i][b] > 10 || x[i][b] == 10) {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = a + 1; i <= 8; i++) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else if (x[i][b] > 10 || x[i][b] == 10) {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = b - 1; i >= 1; i--) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else if (x[a][i] > 10 || x[a][i] == 10) {
                        u[a][i] = 1;
                    }
                    break;
                }
                for (let i = b + 1; i <= 8; i++) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else if (x[a][i] > 10 || x[a][i] == 10) {
                        u[a][i] = 1;
                    }
                    break;
                }
            } else if (x[a][b] == 12) {
                for (let i = 1; i <= 8; i++) {
                    for (let j = 1; j <= 8; j++) {
                        if (x[i][j] == 0 || x[i][j] == 10 || x[i][j] > 10) {
                            if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {

                            }
                        }
                    }
                }
            } else if (x[a][b] == 13) {
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b - i > 0) {
                        if (x[a - i][b - i] == 0 || x[a - i][b - i] == 10) {
                            u[a - i][b - i] = 1;
                            continue;
                        } else if (x[a - i][b - i] > 10 || x[a - i][b - i] == 10) {
                            u[a - i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b + i <= 8) {
                        if (x[a + i][b + i] == 0 || x[a + i][b + i] == 10) {
                            u[a + 1][b + i] = 1;
                            continue;
                        } else if (x[a + i][b + i] > 10 || x[a + i][b + i] == 10) {
                            u[a + 1][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b + i <= 8) {
                        if (x[a - i][b + i] == 0 || x[a - i][b + i] == 10) {
                            u[a - i][b + i] = 1;
                            continue;
                        } else if (x[a - i][b + i] > 10 || x[a - i][b + i] == 10) {
                            u[a - i][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b - i > 0) {
                        if (x[a + i][b - i] == 0 || x[a + i][b - i] == 10) {
                            u[a + i][b - i] = 1;
                            continue;
                        } else if (x[a + i][b - i] > 10 || x[a + i][b - i] == 10) {
                            u[a + i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            } else if (x[a][b] == 19) {
                for (let i = a - 1; i >= 1; i--) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else if (x[i][b] > 10 || x[i][b] == 10) {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = a + 1; i <= 8; i++) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else if (x[i][b] > 10 || x[i][b] == 10) {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = b - 1; i >= 1; i--) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else if (x[a][i] > 10 || x[a][i] == 10) {
                        u[a][i] = 1;
                    }
                    break;
                }
                for (let i = b + 1; i <= 8; i++) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else if (x[a][i] > 10 || x[a][i] == 10) {
                        u[a][i] = 1;
                    }
                    break;
                }
                // tuong
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b - i > 0) {
                        if (x[a - i][b - i] == 0 || x[a - i][b - i] == 10) {
                            u[a - i][b - i] = 1;
                            continue;
                        } else if (x[a - i][b - i] > 10 || x[a - i][b - i] == 10) {
                            u[a - i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b + i <= 8) {
                        if (x[a + i][b + i] == 0 || x[a + i][b + i] == 10) {
                            u[a + i][b + i] = 1;
                            continue;
                        } else if (x[a + i][b + i] > 10 || x[a + i][b + i] == 10) {
                            u[a + i][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b + i <= 8) {
                        if (x[a - i][b + i] == 0 || x[a - i][b + i] == 10) {
                            u[a - i][b + i] = 1;
                            continue;
                        } else if (x[a - i][b + i] > 10 || x[a - i][b + i] == 10) {
                            u[a - i][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b - i > 0) {
                        if (x[a + i][b - i] == 0 || x[a + i][b - i] == 10) {
                            u[a + i][b - i] = 1;
                            continue;
                        } else if (x[a + i][b - i] > 10 || x[a + i][b - i] == 10) {
                            u[a + i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            } else if (x[a][b] == 20) {
                for (let i = 1; i <= 8; i++) {
                    for (let j = 1; j <= 8; j++) {
                        if (x[i][j] == 0) {
                            if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {

                            }
                        } else if (x[i][j] <= 10) {
                            if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b)) {

                            }
                        }
                    }
                }
            }
        }
    }
    return u;
}

function Return() {
    if (h == 1) {
        return;
    }
    h--;
    if (luot == 0) {
        luot = 1; UpDate(1, 9, luot);
    } else {
        luot = 0; UpDate(1, 9, luot);
    }
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            let t = document.getElementById(String(i + "," + j));
            t.classList.remove("tot_den", "xe_den", "ma_den", "tuong_den", "hau_den", "vua_den", "tot_trang", "xe_trang", "ma_trang", "tuong_trang", "hau_trang", "vua_trang", "xanh", "Active");
            x[i][j] = history[h][i][j];
            switch (x[i][j]) {
                case 1: t.classList.add("tot_den");
                    break;
                case 5: t.classList.add("xe_den");
                    break;
                case 2: t.classList.add("ma_den");
                    break;
                case 3: t.classList.add("tuong_den");
                    break;
                case 9: t.classList.add("hau_den");
                    break;
                case 10: t.classList.add("vua_den");
                    break;
                case 11: t.classList.add("tot_trang");
                    break;
                case 15: t.classList.add("xe_trang");
                    break;
                case 12: t.classList.add("ma_trang");
                    break;
                case 13: t.classList.add("tuong_trang");
                    break;
                case 19: t.classList.add("hau_trang");
                    break;
                case 20: t.classList.add("vua_trang");
                    break;
            }
        }
    }
    m = 1;
}
function chieuhet_den() {
    var u = [];
    for (let i = 1; i <= 8; i++) {
        u[i] = new Array();
    }
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if (x[i][j] == 1) {
                if (i == 2) {
                    if (x[i + 1][j] == 0) {
                        u[i + 1][j] = 3;
                        if (x[i + 2][j] == 0) {
                            u[i + 2][j] = 3;
                        }
                    }
                } else {
                    if (x[i + 1][j] == 0) {
                        u[i + 1][j] = 3;
                    }
                }
                if (u[i + 1][j - 1] != 3) {
                    u[i + 1][j - 1] = 2;
                }
                if (u[i + 1][j + 1] != 3) {
                    u[i + 1][j + 1] = 2;
                }
            } else if (x[i][j] == 5) {
                for (let k = i - 1; k >= 1; k--) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = i + 1; k <= 8; k++) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = j - 1; k >= 1; k--) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else {
                        u[i][k] = 1;
                    }
                    break;
                }
                for (let k = j + 1; k <= 8; k++) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else {
                        u[i][k] = 1;
                    }
                    break;
                }
            } else if (x[i][j] == 2) {
                for (let k = 1; k <= 8; k++) {
                    for (let l = 1; l <= 8; l++) {
                        if (x[k][l] == 0 || x[k][l] == 20 || x[k][l] < 10 || x[k][l] > 10) {
                            if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1)) {
                                u[k][l] = 1;
                            }
                        }
                    }
                }
            } else if (x[i][j] == 3) {
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j - k > 0) {
                        if (x[i - k][j - k] == 0 || x[i - k][j - k] == 20) {
                            u[i - k][j - k] = 1;
                            continue;
                        } else {
                            u[i - k][j - k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j + k <= 8) {
                        if (x[i + k][j + k] == 0 || x[i + k][j + k] == 20) {
                            u[i + k][j + k] = 1;
                            continue;
                        } else {
                            u[i + k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j + k <= 8) {
                        if (x[i - k][j + k] == 0 || x[i - k][j + k] == 20) {
                            u[i - k][j + k] = 1;
                            continue;
                        } else {
                            u[i - k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j - k > 0) {
                        if (x[i + k][j - k] == 0 || x[i + k][j - k] == 20) {
                            u[i + k][j - k] = 1;
                            continue;
                        } else {
                            u[i + k][j - k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            } else if (x[i][j] == 9) {
                for (let k = i - 1; k >= 1; k--) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = i + 1; k <= 8; k++) {
                    if (x[k][j] == 0 || x[k][j] == 20) {
                        u[k][j] = 1;
                        continue;
                    } else {
                        u[k][j] = 1;
                    }
                    break;
                }
                for (let k = j - 1; k >= 1; k--) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else {
                        u[i][k] = 1;
                    }
                    break;
                }
                for (let k = j + 1; k <= 8; k++) {
                    if (x[i][k] == 0 || x[i][k] == 20) {
                        u[i][k] = 1;
                        continue;
                    } else {
                        u[i][k] = 1;
                    }
                    break;
                }
                // tuong
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j - k > 0) {
                        if (x[i - k][j - k] == 0 || x[i - k][j - k] == 20) {
                            u[i - k][j - k] = 1;
                            continue;
                        } else {
                            u[i - k][j - k] = 1;
                        }
                        break;
                    } else {
                        break
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j + k <= 8) {
                        if (x[i + k][j + k] == 0 || x[i + k][j + k] == 20) {
                            u[i + k][j + k] = 1;
                            continue;
                        } else {
                            u[i + k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i - k > 0 && j + k <= 8) {
                        if (x[i - k][j + k] == 0 || x[i - k][j + k] == 20) {
                            u[i - k][j + k] = 1;
                            continue;
                        } else {
                            u[i - k][j + k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let k = 1; k <= 8; k++) {
                    if (i + k <= 8 && j - k > 0) {
                        if (x[i + k][j - k] == 0 || x[i + k][j - k] == 20) {
                            u[i + k][j - k] = 1;
                            continue;
                        } else {
                            u[i + k][j - k] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            }
            if (x[i][j] == 10) {
                var e = i;
                var r = j;
            }
        }
    }
    let b = chieutuong_den();
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if ((u[i][j] == 1 && (x[i][j] == 0 || x[i][j] > 10)) || (u[i][j] == 2 && x[i][j] > 10) || (u[i][j] == 3 && x[i][j] == 0)) {
                let w = x[i][j];
                x[i][j] = 4;
                let v = chieutuong_den();
                x[i][j] = w;
                if (v[e][r] != 1) {
                    return 1;
                }
            }
            if ((x[i][j] == 0 || x[i][j] > 10) && b[i][j] != 1) {
                if ((i == e - 1 && j == r - 1) || (i == e && j == r - 1) || (i == e + 1 && j == r - 1) || (i == e + 1 && j == r) || (i == e + 1 && j == r + 1) || (i == e && j == r + 1) || (i == e - 1 && j == r + 1) || (i == e - 1 && j == r)) {
                    return 1;
                }
            }
        }
    }
    return 0;

}
function chieuhet_trang() {
    var u = [];
    for (let i = 1; i <= 8; i++) {
        u[i] = new Array();
    }
    for (let a = 1; a <= 8; a++) {
        for (let b = 1; b <= 8; b++) {
            if (x[a][b] == 11) {
                if (a == 7) {
                    if (x[a - 1][b] == 0) {
                        u[a - 1][b] = 3;
                        if (x[a - 2][b] == 0) {
                            u[a - 2][b] = 3;
                        }
                    }

                } else {
                    if (x[a - 1][b] == 0) {
                        u[a - 1][b] = 3;
                    }
                }
                if (u[a - 1][b - 1] != 3) {
                    u[a - 1][b - 1] = 2;
                }
                if (u[a - 1][b + 1] != 3) {
                    u[a - 1][b + 1] = 2;
                }
            } else if (x[a][b] == 15) {
                for (let i = a - 1; i >= 1; i--) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = a + 1; i <= 8; i++) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = b - 1; i >= 1; i--) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else {
                        u[a][i] = 1;
                    }
                    break;
                }
                for (let i = b + 1; i <= 8; i++) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else {
                        u[a][i] = 1;
                    }
                    break;
                }
            } else if (x[a][b] == 12) {
                for (let i = 1; i <= 8; i++) {
                    for (let j = 1; j <= 8; j++) {
                        if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1)) {
                            u[i][j] = 1;
                        }
                    }
                }
            } else if (x[a][b] == 13) {
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b - i > 0) {
                        if (x[a - i][b - i] == 0 || x[a - i][b - i] == 10) {
                            u[a - i][b - i] = 1;
                            continue;
                        } else {
                            u[a - i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b + i <= 8) {
                        if (x[a + i][b + i] == 0 || x[a + i][b + i] == 10) {
                            u[a + 1][b + i] = 1;
                            continue;
                        } else {
                            u[a + 1][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b + i <= 8) {
                        if (x[a - i][b + i] == 0 || x[a - i][b + i] == 10) {
                            u[a - i][b + i] = 1;
                            continue;
                        } else {
                            u[a - i][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b - i > 0) {
                        if (x[a + i][b - i] == 0 || x[a + i][b - i] == 10) {
                            u[a + i][b - i] = 1;
                            continue;
                        } else {
                            u[a + i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            } else if (x[a][b] == 19) {
                for (let i = a - 1; i >= 1; i--) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = a + 1; i <= 8; i++) {
                    if (x[i][b] == 0 || x[i][b] == 10) {
                        u[i][b] = 1;
                        continue;
                    } else {
                        u[i][b] = 1;
                    }
                    break;
                }
                for (let i = b - 1; i >= 1; i--) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else {
                        u[a][i] = 1;
                    }
                    break;
                }
                for (let i = b + 1; i <= 8; i++) {
                    if (x[a][i] == 0 || x[a][i] == 10) {
                        u[a][i] = 1;
                        continue;
                    } else {
                        u[a][i] = 1;
                    }
                    break;
                }
                // tuong
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b - i > 0) {
                        if (x[a - i][b - i] == 0 || x[a - i][b - i] == 10) {
                            u[a - i][b - i] = 1;
                            continue;
                        } else {
                            u[a - i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b + i <= 8) {
                        if (x[a + i][b + i] == 0 || x[a + i][b + i] == 10) {
                            u[a + i][b + i] = 1;
                            continue;
                        } else {
                            u[a + i][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a - i > 0 && b + i <= 8) {
                        if (x[a - i][b + i] == 0 || x[a - i][b + i] == 10) {
                            u[a - i][b + i] = 1;
                            continue;
                        } else {
                            u[a - i][b + i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
                for (let i = 1; i <= 8; i++) {
                    if (a + i <= 8 && b - i > 0) {
                        if (x[a + i][b - i] == 0 || x[a + i][b - i] == 10) {
                            u[a + i][b - i] = 1;
                            continue;
                        } else {
                            u[a + i][b - i] = 1;
                        }
                        break;
                    } else {
                        break;
                    }
                }
            }
            if (x[a][b] == 20) {
                var e = a;
                var r = b;
            }
        }
    }
    let b = chieutuong_trang();
    for (let i = 1; i <= 8; i++) {
        for (let j = 1; j <= 8; j++) {
            if ((u[i][j] == 1 && (x[i][j] == 0 || x[i][j] < 10)) || (u[i][j] == 2 && x[i][j] < 10 && x[i][j] != 0) || (u[i][j] == 3 && x[i][j] == 0)) {
                let w = x[i][j];
                x[i][j] = 14;
                let v = chieutuong_trang();
                x[i][j] = w;
                if (v[e][r] != 1) {
                    return 1;
                }
            }
            if ((x[i][j] == 0 || x[i][j] <= 10) && b[i][j] != 1) {
                if ((i == e - 1 && j == r - 1) || (i == e && j == r - 1) || (i == e + 1 && j == r - 1) || (i == e + 1 && j == r) || (i == e + 1 && j == r + 1) || (i == e && j == r + 1) || (i == e - 1 && j == r + 1) || (i == e - 1 && j == r)) {
                    return 1;
                }
            }
        }
    }
    return 0;
}