<?php

namespace Database\Seeders;

use App\Models\Post;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class PostSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        Post::create(
            [
                'user_id' => "1",
                'post_template_id' => "1",
                'content' => "Đây là nội dung bài viết số 1",
                'post_status_id' => 1
            ]
        );
    }
}