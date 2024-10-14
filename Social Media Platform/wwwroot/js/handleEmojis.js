
var html = emojis.map(function (emoji) {
    return `<span onclick="func(event)" class="emoji">${emoji.label}</span>`;
});

var emojilist = document.querySelector('.emojiDropdown');
var _emojiTempHtml = '';
var countPerGroup = 8;
var lastOne = html.length - 1;

var count = 0
var insertToDom = (str, i) => {
  (() => {
      setTimeout(() => {
      emojilist.innerHTML += str;
    }, i * 0 / countPerGroup);
  })(str, i);
};

html.forEach((item, i) => {
    _emojiTempHtml += item;
    if ((i+1) % 4 ==0) _emojiTempHtml += "<br/>"
  if ((i + 1) % countPerGroup === 0 || i === lastOne) {
    insertToDom(_emojiTempHtml, i + 1);
    _emojiTempHtml = '';
  }
});





var textarea = document.getElementById("textarea");
var func = (event) =>{
  
  textarea.value  += event.target.innerHTML;
    event.stopPropagation();
}


