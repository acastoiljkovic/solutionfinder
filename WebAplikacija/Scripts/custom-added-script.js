function changeActive(elem) {
    var el = document.getElementsByTagName("a");

    for (let i = 0; i < el.length; i++)
        el[i].classList.remove("active");

    elem.classList.add("active");
}
