$("#login").validate({
	rules: {
		password: "required",
		login: "required",
		username: {
			required: true,
			minlength: 2
		},
		password: {
			required: true,
			minlength: 5
		}
	},
	messages: {
		username: {
			required: "Please enter a username",
			minlength: "Your username must consist of at least 2 characters"
		},
		password: {
			required: "Please provide a password",
			minlength: "Your password must be at least 5 characters long"
		}
	},
	submitHandler: function () {
		$("#login").submit();
	}
});

$("#register").validate({
	rules: {
		password: "required",
		passwordRep: "required",
		login: "required",
		username: {
			required: true,
			minlength: 2
		},
		password: {
			required: true,
			minlength: 5
		},
		passwordRep: {
			required: true,
			minlength: 5,
			equalTo: '#password'
		}
	},
	messages: {
		username: {
			required: "Please enter a username",
			minlength: "Your username must consist of at least 2 characters"
		},
		password: {
			required: "Please provide a password",
			minlength: "Your password must be at least 5 characters long"
		},
		passwordRep: {
			required: "Please provide a password",
			minlength: "Your password must be at least 5 characters long",
			equalTo: "The password doesn't match the one above"
		}
	},
	submitHandler: function () {
		$("#register").submit();
	}
});