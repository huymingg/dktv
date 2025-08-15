function generateCaptcha() {

    const canvas = document.getElementById("captcha");
    const ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    let captchaCode = "";

    // Nền trắng
    ctx.fillStyle = "#fff";
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    // Tạo nhiễu nhẹ
    for (let i = 0; i < 8; i++) {
        ctx.strokeStyle = `rgba(${Math.floor(Math.random() * 256)},${Math.floor(
            Math.random() * 256
        )},${Math.floor(Math.random() * 256)},0.2)`;
        ctx.beginPath();
        ctx.moveTo(Math.random() * canvas.width, Math.random() * canvas.height);
        ctx.lineTo(Math.random() * canvas.width, Math.random() * canvas.height);
        ctx.stroke();
    }

    ctx.textBaseline = "middle";
    ctx.textAlign = "center";

    for (let i = 0; i < 4; i++) {
        let char = chars.charAt(Math.floor(Math.random() * chars.length));
        captchaCode += char;

        let fontSize = Math.floor(Math.random() * 10) + 28; // 28px - 38px
        ctx.font = `bold ${fontSize}px 'Inter', sans-serif`;

        ctx.fillStyle = `rgb(${Math.floor(Math.random() * 256)},${Math.floor(
            Math.random() * 256
        )},${Math.floor(Math.random() * 256)})`;

        let x = 30 + i * 40;
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

generateCaptcha()