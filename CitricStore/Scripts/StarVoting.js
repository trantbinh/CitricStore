const percentage = Math.round((value / max) * 100);
document.querySelector('.overlay').style.width = `${100 - percentage}%`;
