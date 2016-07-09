create table posts (
	post_id serial primary key,
	title text not null,
	content text not null,
	created_time timestamptz not null
);
