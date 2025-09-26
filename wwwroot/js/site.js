// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Load categories into the table

function loadCategories() {
    $.get(API_BASE_URL + "api/categories", function (data) {
        
        let tbody = $("#categoriesTable tbody");
        tbody.empty();

        data.forEach(category => {
           
            let row = `
                <tr>
                    <td>${category.id}</td>
                    <td>${category.categoryName}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="editCategory(${category.id})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteCategory(${category.id})">Delete</button>
                    </td>
                </tr>
            `;
            tbody.append(row);
        });
    });
}

// Delete category with confirmation
function deleteCategory(id) {

    if (!confirm("Are you sure you want to delete this category?")) return;

    $.ajax({
        url: API_BASE_URL + `api/categories/${id}`,
        type: 'DELETE',
        success: function () {
            showSuccessToast("Category deleted successfully!");
            loadCategories(); // Refresh the table dynamically
        },
        error: function () {
            showErrorToast("Failed to delete category.");
        }
    });
}

// Load issues into the table
function loadIssues() {
    $.get(API_BASE_URL + "/api/issues", function (data) {
        let tbody = $("#issuesTable tbody");
        tbody.empty();

        data.forEach(issue => {
            let row = `
                <tr>
                    <td>${issue.id}</td>
                    <td>${issue.title}</td>
                    <td>${issue.description}</td>
                    <td>${issue.status}</td>
                    <td>${issue.priority}</td>
                    <td>${issue.projectId}</td>
                    <td>${issue.assignedToUserId}</td>
                    <td>${issue.createdDate}</td>
                    <td>
                        <a href="/Issues/Edit?id=${issue.id}" class="btn btn-sm btn-warning">Edit</a>
                        <button class="btn btn-danger btn-sm" onclick="deleteIssue(${issue.id})">Delete</button>
                    </td>
                </tr>
            `;

            tbody.append(row);
        });
    });
}

// Delete issue with confirmation
function deleteIssue(id) {
    if (!confirm("Are you sure want to delete this issue?")) {
        return;
    }

    $.ajax({
        url: API_BASE_URL + `/api/issues/${id}`,
        type: `DELETE`,
        success: function (data) {
            showSuccessToast("Issue deleted successfully!");
            loadIssues(); // refresh the table
        },
        error: function (error) {
            showErrorToast("Failed to delete issue");
        }
    });
}

// Success toast
function showSuccessToast(message) {
    $(document).Toasts('create', {
        class: 'bg-success',
        title: 'Success',
        body: message,
        autohide: true,
        delay: 1000
    });
}

// Error toast
function showErrorToast(message) {
    $(document).Toasts('create', {
        class: 'bg-danger',
        title: 'Error',
        body: message
    });
}

// Call this when page is loaded
$(document).ready(function () {

});

