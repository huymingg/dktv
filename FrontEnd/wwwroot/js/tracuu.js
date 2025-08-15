document.addEventListener('DOMContentLoaded', function () {

    // --- TỰ ĐỘNG KIỂM TRA VÀ HIỂN THỊ KẾT QUẢ ---
    const storedData = sessionStorage.getItem('lookupData');
    if (storedData) {
        try {
            const lookupData = JSON.parse(storedData);

            // Ẩn form tra cứu đi
            const lookupForm = document.querySelector('.lookup-form');
            if (lookupForm) lookupForm.style.display = 'none';

            // Điền thông tin vào khu vực kết quả
            document.getElementById('result-reg-code').textContent = lookupData.registrationCode;
            document.getElementById('result-status').textContent = lookupData.status;

            // Điền thông tin lên thẻ mô phỏng
            document.getElementById('card-photo').src = lookupData.photoData;
            document.getElementById('card-fullname').textContent = lookupData.fullName;

            // Định dạng lại ngày sinh từ ISO string sang DD/MM/YYYY
            const dob = new Date(lookupData.dob);
            const formattedDob = ('0' + dob.getDate()).slice(-2) + '/' + ('0' + (dob.getMonth() + 1)).slice(-2) + '/' + dob.getFullYear();
            document.getElementById('card-dob').textContent = formattedDob;

            // Hiển thị khu vực kết quả
            document.getElementById('lookup-results').style.display = 'block';

            // Xóa dữ liệu sau khi đã dùng để lần sau F5 trang sẽ không hiện lại
            sessionStorage.removeItem('lookupData');

        } catch (e) {
            console.error("Lỗi xử lý dữ liệu tra cứu:", e);
            sessionStorage.removeItem('lookupData');
        }
    }

    // --- PHẦN XỬ LÝ CAPTCHA ---
    let currentCatchaCode = '';

    function generateCaptcha() {
        const canvas = document.getElementById("captcha");
        if (!canvas) return;
        const ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        let captchaCode = "";
        ctx.fillStyle = "#fff";
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.textBaseline = "middle";
        ctx.textAlign = "center";
        for (let i = 0; i < 4; i++) {
            let char = chars.charAt(Math.floor(Math.random() * chars.length));
            captchaCode += char;
            let fontSize = Math.floor(Math.random() * 10) + 28;
            ctx.font = `bold ${fontSize}px 'Inter', sans-serif`;
            ctx.fillStyle = `rgb(${Math.floor(Math.random() * 150)},${Math.floor(Math.random() * 150)},${Math.floor(Math.random() * 150)})`;
            let x = 35 + i * 50;
            let y = canvas.height / 2 + (Math.random() * 20 - 10);
            let angle = (Math.random() * 30 - 15) * (Math.PI / 180);
            ctx.save();
            ctx.translate(x, y);
            ctx.rotate(angle);
            ctx.fillText(char, 0, 0);
            ctx.restore();
        }
        currentCatchaCode = captchaCode;
    }

    generateCaptcha();
});
