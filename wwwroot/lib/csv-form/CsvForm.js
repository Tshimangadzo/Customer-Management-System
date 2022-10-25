
function submitCsv() {

		if ($("#csvFile").val() == '') {
			alert('Please select a file.');
			return false;
	     }

	    const files = $('#csvFile').get(0).files;
	const url = "Customers/processCsv";
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
				console.log(repo)
			},
			error: function (err) {
				console.log(err)
			}
		}

	jQuery.ajax(data);
 }

