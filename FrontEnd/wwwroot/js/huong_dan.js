document.addEventListener("DOMContentLoaded", function() {
    const accordionHeaders = document.querySelectorAll(".accordion-header");

    accordionHeaders.forEach(header => {
        header.addEventListener("click", function() {
            const isActive = this.classList.contains("active");

            // Đóng tất cả các mục khác
            accordionHeaders.forEach(h => {
                h.classList.remove("active");
                if (h.nextElementSibling) {
                    h.nextElementSibling.style.display = "none";
                }
            });

            // Nếu mục vừa click không phải là mục đang active, thì mở nó ra
            if (!isActive) {
                this.classList.add("active");
                if (this.nextElementSibling) {
                    this.nextElementSibling.style.display = "block";
                }
            }
        });
    });
    
    // Mở mục Video Hướng Dẫn mặc định
    if(accordionHeaders.length > 4) {
        const videoHeader = accordionHeaders[4];
        videoHeader.classList.add("active");
        if (videoHeader.nextElementSibling) {
            videoHeader.nextElementSibling.style.display = "block";
        }
    }
});