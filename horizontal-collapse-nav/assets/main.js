! function() {
  "use strict";
console.log(e);
  function e() {
    console.log(c);
    c.classList.remove("disabled"), 
    n.classList.remove("disabled"), 
    l.scrollLeft <= 0 && c.classList.add("disabled"), 
    l.scrollLeft + l.clientWidth + 5 >= l.scrollWidth && n.classList.add("disabled");
  }

  function t(e) {
    l.scrollLeft += e
  }
  var n = document.querySelector(".scrollindicator.scrollindicator--right"),
    c = document.querySelector(".scrollindicator.scrollindicator--left"),
    l = document.querySelector(".docs-navigation"),
    i = 40;
  l.addEventListener("scroll", e), e(),
  n.addEventListener("click", t.bind(null, i)), 
  n.addEventListener("tap", t.bind(null, i)), 
  c.addEventListener("click", t.bind(null, -i)), 
  c.addEventListener("tap", t.bind(null, -i));
}();