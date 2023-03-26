'use strict'; 

function getAntiForgeryToken() {
    return document.querySelector('meta[name="X-CSRF-TOKEN"]').content;
}