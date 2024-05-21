/*
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