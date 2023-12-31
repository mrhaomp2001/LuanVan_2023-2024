<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('post_templates', function (Blueprint $table) {
            $table->id();
            $table->string("name");
            $table->string("content");
            $table->string("theme_color");
            $table->boolean("is_require_title");
            $table->boolean("is_require_image");
            $table->boolean("can_use");
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('post_templates');
    }
};
