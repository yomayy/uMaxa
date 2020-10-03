var app = new Vue({
	el: '#app',
	data: {
		status: 0,
		loading: false,
		orders: [],
		selectedOrder: null
	},
	mounted() {
		this.getOrders();
	},
	watch: {
		status: function () {
			this.getOrders();
		}
	},
	methods: {
		getOrders() {
			this.loading = true
			axios.get('/orders?status=' + this.status)
				.then(result => {
					this.orders = result.data;
					this.loading = false;
				})
				.catch(err => {
					console.log(err);
				})
		},
		selectOrder(id) {
			this.loading = true
			axios.get('/orders/' + id)
				.then(result => {
					this.selectedOrder = result.data;
					this.loading = false;
				})
				.catch(err => {
					console.log(err);
				})
		},
		updateOrder() {
			this.loading = true;
			axios.put('/orders/' + this.selectedOrder.id, null)
				.then(result => {
					console.log(result.data);
					this.loading = false;
					this.exitOrder();
					//
					//this.sendEmail();
					//
					this.getOrders();
					// todo: email
				})
				.catch(err => {
					console.log(err);
				})
		},
		sendEmail() {
			axios.post('/orders/' + this.selectedOrder.id + '/send', null)
				.then(result => {
					console.log(result.data);
					this.loading = false;

				})
				.catch(err => {
					console.log(err);
				})
		},
		exitOrder() {
			this.selectedOrder = null;
		}
	},
	computed: {
	}
});