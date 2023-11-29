document.addEventListener("DOMContentLoaded", function () {
    var currentPath = window.location.pathname;

    console.log("Current Path:", currentPath);

    var links = document.querySelectorAll('.sidebar a');

    links.forEach(function (link) {
        var linkPath = link.getAttribute('href');

        console.log("Link Path:", linkPath);

        if (currentPath == linkPath) {
            link.classList.add('active');
        }
    });
});

const sideMenu = document.querySelector('aside');
const menuBtn = document.getElementById('menu-btn');
const closeBtn = document.getElementById('close-btn');

const darkMode = document.querySelector('.dark-mode');

menuBtn.addEventListener('click', () => {
    sideMenu.style.display = 'block';
});

closeBtn.addEventListener('click', () => {
    sideMenu.style.display = 'none';
});

darkMode.addEventListener('click', () => {
    document.body.classList.toggle('dark-mode-variables');
    darkMode.querySelector('span:nth-child(1)').classList.toggle('active');
    darkMode.querySelector('span:nth-child(2)').classList.toggle('active');
})

//Marking With Selection

function adminDeleteWeatherAsBatch() {
    var checkboxes = document.getElementsByName('checkboxes');
    var selectedCheckboxes = [];

    checkboxes.forEach(function (checkbox) {
        if (checkbox.checked) {
            selectedCheckboxes.push(checkbox.value);
        }
    });

    if (selectedCheckboxes.length > 0) {
        var form = document.createElement('form');
        form.method = 'POST';
        form.action = '/Admin/Weather/AdminDeleteWeatherAsBatch';

        selectedCheckboxes.forEach(function (id) {
            var input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'checkboxes';
            input.value = id;
            form.appendChild(input);
        });

        document.body.appendChild(form);
        form.submit();
    } else {
        // No checkboxes selected
    }
}

function adminDeleteUserAsBatch() {
    var checkboxes = document.getElementsByName('checkboxes');
    var selectedCheckboxes = [];

    checkboxes.forEach(function (checkbox) {
        if (checkbox.checked) {
            selectedCheckboxes.push(checkbox.value);
        }
    });

    if (selectedCheckboxes.length > 0) {
        var form = document.createElement('form');
        form.method = 'POST';
        form.action = '/Admin/User/AdminDeleteUserAsBatch';

        selectedCheckboxes.forEach(function (id) {
            var input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'checkboxes';
            input.value = id;
            form.appendChild(input);
        });

        document.body.appendChild(form);
        form.submit();
    } else {
        // No checkboxes selected
    }
}

//Checkbox Selector

function changeBackground(checkbox) {
    var tr = checkbox.closest('tr');
    if (checkbox.checked) {
        tr.classList.add('checked');
    } else {
        tr.classList.remove('checked');
    }
}

function selectAllRows() {
    var allCheckbox = document.getElementById('All');
    var tbodyCheckboxes = document.querySelectorAll('tbody input[type="checkbox"]');

    for (var i = 0; i < tbodyCheckboxes.length; i++) {
        var checkbox = tbodyCheckboxes[i];
        checkbox.checked = allCheckbox.checked;

        var tr = checkbox.closest('tr');
        if (allCheckbox.checked) {
            tr.classList.add('checked');
        } else {
            tr.classList.remove('checked');
        }
    }
}


//Dynamic Search

$(document).ready(function () {
    // Trigger the search whenever the search input changes
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().toLowerCase();

        // Loop through each row in the table
        $('#tableBody tr').each(function () {
            var rowText = $(this).text().toLowerCase();

            // Show or hide the row based on the search input match
            $(this).toggle(rowText.indexOf(searchText) > -1);
        });
    });
});


//Modal

const openButton = document.querySelector("[data-open-modal]")
const closeButton = document.querySelector("[data-close-modal]")
const modal = document.querySelector("[data-modal]")

openButton.addEventListener("click", () => {
    modal.showModal()
})

closeButton.addEventListener("click", () => {
    modal.close()
})
