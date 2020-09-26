var app = new Vue({
	el: '#app',
	data: {
		editing: false,
		loading: false,
		objectIndex: 0,
		categoryModel: {
			id: 00000000 - 0000 - 0000 - 0000 - 000000000000,
			name: "CategoryName",
			description: "Category Description"
		},
		categories: [],
		//selectedCategory: {
		//	id: 0,
		//	name: "",
		//	description: "",
		//	products: []
		//},
		selectedCategory: null,
		newProduct: {
			categoryId: 0,
			name: "Product name",
			description: "Product description",
			value: 1.99
		}
	},
	mounted() {
		this.getCategories();
	},
	methods: {
		getCategory(id) {
			this.loading = true
			axios.get('/categories/' + id)
				.then(res => {
					console.log(res);
					var category = res.data;
					this.categoryModel = {
						id: category.id,
						name: category.name,
						description: category.description
					};
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
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
		createCategory() {
			this.loading = true;
			axios.post('/categories', this.categoryModel)
				.then(res => {
					console.log(res.data);
					this.categories.push(res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.editing = false;
				})
		},
		updateCategory() {
			this.loading = true;
			axios.put('/categories', this.categoryModel)
				.then(res => {
					console.log(res.data);
					this.categories.splice(this.objectIndex, 1, res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.editing = false;
				})
		},
		deleteCategory(id, index) {
			this.loading = true
			axios.delete('/categories/' + id)
				.then(res => {
					console.log(res);
					this.categories.splice(index, 1);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		newCategory() {
			this.editing = true;
			this.categoryModel.id = 0;
		},
		editCategory(id, index) {
			this.objectIndex = index;
			this.getCategory(id);
			this.editing = true;
		},
		cancel() {
			this.editing = false;
		},
		selectCategory(category) {
			console.log(category);
			this.selectedCategory = category;
			this.newProduct.categoryId = category.id;
		},
		newProduct() {

		}
	},
	computed: {
	}
})