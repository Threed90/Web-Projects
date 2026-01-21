

document.addEventListener("DOMContentLoaded", function () {
    const deleteForms = document.querySelectorAll(".delete-form");

    deleteForms.forEach(form => {
        form.addEventListener("submit", function (e) {
            e.preventDefault();

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
        });
    });
});



document.addEventListener("DOMContentLoaded", function () {
    const editButtons = document.querySelectorAll(".edit-form-btn");

    editButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault(); 

            Swal.fire({
                title: "Are you sure?",
                text: "You will make changes on the Post!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, edit it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    button.closest("form").submit(); 
                }
            });
        });
    });
});

