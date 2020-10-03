var app = new Vue({
	el: '#app',
	data: {
		editing: false,
		loading: false,
		categoryFlag: false,
		categories: [],
		products: [],
		selectedCategory: null,
		categoryName: "All categories"
	},
	mounted() {
		//this.getCategories();
		//this.getProducts();
	},
	methods: {
		setCategoryName(name) {
			this.categoryName = name;
		},
		getCId(id) {
			return id;
		},
		getProducts() {
			this.loading = true
			axios.get('/products')
				.then(res => {
					console.log(res);
					this.products = res.data;
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.getCategories();
				});
		},
		getCategories() {
			this.loading = true
			axios.get('/categories')
				.then(res => {
					console.log(res);
					this.categories = res.data;
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		selectCategory(id){
			axios.get('?categoryId=' + id)
				.then(res => {
					console.log(res);
					this.categories = res.data;
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