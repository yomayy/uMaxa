var app = new Vue({
	el: '#app',
	data: {
		editing: false,
		loading: false,
		username: "",
		isAdmin: false,
		selectedUser: null,
		selectedIndex: 0,
		users: []
	},
	mounted() {
		this.getUsers();
	},
	methods: {
		createUser() {
			this.loading = true;
			axios.post('/users', { username: this.username })
				.then(res => {
					console.log(res);
					this.users.push(res.data);
					this.loading = false;
					this.editing = false;
				})
				.catch(err => {
					console.log(err);
				});
		},
		getUsers() {
			this.loading = true
			axios.get('/users')
				.then(res => {
					console.log(res);
					this.users = res.data;
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		updateUser() {
			this.loading = true;
			axios.put('/users', this.selectedUser)
				.then(res => {
					console.log(res.data);
					this.users.splice(this.selectedIndex, 1, res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.editing = false;
				})
		},
		deleteUser(id, index) {
			this.loading = true;
			axios.delete('/users/' + id)
				.then(res => {
					console.log(res);
					this.users.splice(index, 1);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.selectedIndex = 0;
					this.selectedUser = null;
				});
		},
		selectUser(user, index) {
			this.selectedUser = user;
			this.selectedIndex = index;
		},
		checkAdmin(user) {
			if (user.userName == 'Admin'
				|| user.userName == 'Manager') {
				return true;
			} else {
				return false;
			}
		},
		cancel() {
			this.editing = false;
		}
	}
})