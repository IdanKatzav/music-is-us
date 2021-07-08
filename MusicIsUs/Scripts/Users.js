$.validator.addMethod("passwordCheck", function (value) {
	return /^[A-Za-z0-9\d=!\-@._*]*$/.test(value) // consists of only these
		&& /[a-z]/.test(value) // has a lowercase letter
		&& /[A-Z]/.test(value) // has a lowercase letter
		&& /\d/.test(value) // has a digit
		&& /[!#@%$&^]/.test(value) // has signs
});

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
			minlength: "Your password must be at least 8 characters long"
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
			minlength: 4
		},
		password: {
			required: true,
			minlength: 8,
			passwordCheck: true
		},
		passwordRep: {
			required: true,
			minlength: 8,
			equalTo: '#password'
		}
	},
	messages: {
		username: {
			required: "Please enter a username",
			minlength: "Your username must consist of at least 4 characters"
		},
		password: {
			required: "Please provide a password",
			minlength: "Your password must be at least 8 characters long",
			passwordCheck: "Password needs to have lowercase letters, uppercase letters, numbers and signs"
		},
		passwordRep: {
			required: "Please provide a password",
			minlength: "Your password must be at least 8 characters long",
			equalTo: "The password doesn't match the one above"
		}
	},
	submitHandler: function () {
		$("#register").submit();
	}
});