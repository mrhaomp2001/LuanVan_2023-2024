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
        Schema::create('question_collections', function (Blueprint $table) {
            $table->id();
            $table->integer("classroom_id");
            $table->string("name");
            $table->string("difficulty");
            $table->string("game_type");
            $table->integer("questions_per_time");
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('question_collections');
    }
};
