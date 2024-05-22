﻿/*
function handleClickCat(event, controllerName, categoryId) {
  event.preventDefault();
  fetch(`/${controllerName}/filter?categoryId=${categoryId}`, {
    method: 'GET',
    headers: {
      'X-Requested-With': 'XMLHttpRequest'
    }
  })
    .then(response => response.text())
    .then(html => {
      console.log(html);
      window.location.pathname = `/${controllerName}/index`;
      document.querySelector(`.${controllerName}-area #${controllerName}s-row`).innerHTML = html;
    })
    .catch(error => {
      console.error('Error fetching filtered data:', error);
    });
}

function handleClickTag(event, controllerName, tagId) {
  event.preventDefault();
  fetch(`/${controllerName}/filter?tagId=${tagId}`, {
    method: 'GET',
    headers: {
      'X-Requested-With': 'XMLHttpRequest'
    }
  })
    .then(response => response.text())
    .then(html => {
      console.log(html);
      window.location.pathname = `/${controllerName}/index`;
      document.querySelector(`.${controllerName}-area #${controllerName}s-row`).innerHTML = html;
    })
    .catch(error => {
      console.error('Error fetching filtered data:', error);
    });
}
*/

$(document).ready(function () {
  const input = $('.search-form .search-input');
  const ul = $('.search-form .search-results');
  const url = '/Home/SearchAutocomplete';

  input.keyup(function () {
    const value = input.val();
    if (value.length > 0) {
      $.ajax({
        url: url,
        type: 'GET',
        data: { query: value },
        success: function (data) {
          console.log(data);
          ul.empty();
          ul.append(`<li class="list-group-item active"><a href="/course/index">COURSES</a></li>`);
          data.courses.forEach(function (item) {
            ul.append(`<li class="list-group-item"><a href="/course/details/${item.id}">${item.name}</a></li>`);
          });
          ul.append(`<li class="list-group-item active"><a href="/event/index">EVENTS</a></li>`);
          data.events.forEach(function (item) {
            ul.append(`<li class="list-group-item"><a href="/event/details/${item.id}">${item.name}</a></li>`);
          });
          ul.append(`<li class="list-group-item active"><a href="/blog/index">BLOGS</a></li>`);
          data.blogs.forEach(function (item) {
            ul.append(`<li class="list-group-item"><a href="/blog/details/${item.id}">${item.name}</a></li>`);
          });
        },
        error: function (error) {
          console.error('Error fetching autocomplete data:', error);
        }
      });
    } else {
      ul.empty();
    }
  });
  function showToast(message) {
    const toastModel = document.getElementById('liveToast')
    toastModel.querySelector('.toast-body').innerText = message
    const toast = bootstrap.Toast.getOrCreateInstance(toastModel)
    toast.show()
  }

  document.getElementById('Subscribe-Form').addEventListener('submit', function (event) {
    event.preventDefault();
    showToast("Processing...");
    const email = $("#EmailAddress-Input").val();
    fetch(`/Home/Subscribe?email=${email}`)
      .then(response => response.json().then(data => ({
        status: response.status,
        body: data
      })))
      .then(({ status, body }) => {
        if (body.success) {
          showToast(body.message);
        } else {
          showToast("Error: " + (body.message || "Unknown error occurred"));
        }
      })
      .catch(error => {
        console.error('Error:', error);
        showToast("An error occurred. Please try again.");
      });
  });
});