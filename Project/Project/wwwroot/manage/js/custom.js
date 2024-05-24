$(document).ready(function () {
  $('.delete-btn').click(function (e) {
    e.preventDefault();
    const url = $(this).attr('href');
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        swalWithBootstrapButtons.fire({
          title: "Deleted!",
          text: "Your file has been deleted.",
          icon: "success"
        }).then((result) => {
          if (result.isConfirmed) {
            window.location.href = url;
          }
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          text: "The item is safe :)",
          icon: "error"
        });
      }
    });
  });
  $(".imgInput").change(function (e) {
    let box = $(this).parent().find(".preview-box");
    $(box).find(".previewImg").remove();

    for (var i = 0; i < e.target.files.length; i++) {

      let img = document.createElement("img");
      img.style.width = "200px";
      img.classList.add("previewImg");

      let reader = new FileReader();
      console.log(e.target.nextElementSibling);
      reader.readAsDataURL(e.target.files[i]);
      reader.onload = () => {
        img.setAttribute("src", reader.result);
        $(box).append(img)
      }
    }
  })

  $(".remove-img-icon").click(function () {
    $(this).parent().remove();
  })
});