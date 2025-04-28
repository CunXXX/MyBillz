
export function toggleFaqAnswer(faqId) {
    const answer = document.getElementById(faqId);
    const arrow = answer.previousElementSibling.querySelector(".arrow");

    if (!answer) return;

    if (answer.style.maxHeight) {
        // 收起
        answer.style.maxHeight = null;
        arrow.style.transform = "rotate(0deg)";
    } else {
        // 展開
        answer.style.maxHeight = answer.scrollHeight + "px";
        arrow.style.transform = "rotate(180deg)";
    }
}
