<?php

namespace Database\Seeders;

use App\Models\Comment;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class CommentSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        Comment::create(
            [
                'post_id' => "1",
                'user_id' => "1",
                'content' => "Bình luận 1, bởi user 1, ở bài đăng 1",
                'comment_status_id' => "1",
            ]
        );
    }
}