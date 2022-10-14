
function submitCsv() {
		const files = $('#csvFile').prop("files");
	    const url = '@Url.Action("ProcessCsv","Customers")'
		formData = new FormData();
	    formData.append("file", files[0]);

		jQuery.ajax({
			type: 'POST',
			url: url,
			data: formData,
			cache: false,
			contentType: false,
			processData: false,
			success: function (repo) {
				if (repo.status == "success") {
					alert("File : " + repo.filename + " is uploaded successfully");
				}
			},
			error: function (err) {
				alert("Error occurs : ", err);
			}
        });
 }

