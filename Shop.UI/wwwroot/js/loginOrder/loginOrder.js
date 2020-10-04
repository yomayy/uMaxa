var app = new Vue({
	el: '#app',
	data: {
		loading: false,
		orderModel: [],
	},
	mounted() {

	},
	methods: {
		getOrders(email) {
			this.loading = true;
			axios.get('/UserOrder', { params: {email: email} })
				.then(res => {
					console.log(res);
					this.orderModel = res.data
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		}
	}
})