<?php

namespace Database\Seeders;

use App\Models\Post;
use Carbon\Carbon;
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
        for ($i = 1; $i < 10; $i++) {
            # code...
            Post::create(
                [
                    'user_id' => "1",
                    'post_template_id' => "1",
                    'content' => "Đây là nội dung bài viết số 1",
                    'title' => "tiêu đề bài viết 1",
                    'post_status_id' => 1,
                    'created_at' => Carbon::now()->subMinutes($i)
                ]
            );
        }
    }
}