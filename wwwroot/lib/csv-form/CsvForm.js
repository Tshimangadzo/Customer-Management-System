
function submitCsv() {

		if ($("#csvFile").val() == '') {
			alert('Please select a file.');
			return false;
	     }

	    const files = $('#csvFile').get(0).files;
	    const url = "ProcessCsv";
		var formData = new FormData();
		formData.append("file", files[0]);

	    const data = {
	 		type: 'POST',
			url: url,
			data: formData,
			dataType: 'json',
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
		}

	jQuery.ajax(data);
 }

